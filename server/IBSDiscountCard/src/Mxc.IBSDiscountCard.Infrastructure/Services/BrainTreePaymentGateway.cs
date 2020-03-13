using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Braintree;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mxc.Helpers.DateTime;
using Mxc.IBSDiscountCard.Application.User.Responses;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;
using Subscription = Braintree.Subscription;

namespace Mxc.IBSDiscountCard.Infrastructure.Services
{
    public class BrainTreePaymentGateway : IPaymentGateway
    {
        private readonly PaymentOptions _options;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<BrainTreePaymentGateway> _logger;
        private readonly BraintreeGateway _gateway;

        public BrainTreePaymentGateway(IOptions<PaymentOptions> options,
            IDateTimeHelper dateTimeHelper,
            IUserRepository userRepository,
            ILogger<BrainTreePaymentGateway> logger)
        {
            _options = options.Value;
            _dateTimeHelper = dateTimeHelper;
            _userRepository = userRepository;
            _logger = logger;
            _gateway = new BraintreeGateway
            {
                Environment = options.Value.Environment == Braintree.Environment.PRODUCTION.ToString()
                    ? Braintree.Environment.PRODUCTION
                    : Braintree.Environment.SANDBOX,
                MerchantId = options.Value.MerchantId,
                PublicKey = options.Value.PublicKey,
                PrivateKey = options.Value.PrivateKey
            };
        }

        public async Task<string> CreateCustomerAsync(string fullname, string email)
        {
            var names = fullname.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var request = new CustomerRequest
            {
                FirstName = names[0],
                LastName = names.Length > 1 ? string.Join(" ", names.Skip(1)) : "",
                Email = email,
            };

            Result<Customer> result = await _gateway.Customer.CreateAsync(request);

            if (result.IsSuccess())
            {
                return result.Target.Id;
            }

            throw new IBSDiscountCardApiErrorException(FunctionCodes.BraintreeCustomerCreation);
        }

        public async Task<string> GenerateClientTokenAsync(string customerId)
        {
            try
            {
                return await _gateway.ClientToken.GenerateAsync(
                    new ClientTokenRequest
                    {
                        CustomerId = customerId
                    }
                );
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while generating braintree client token");

                throw new IBSDiscountCardApiErrorException($"Error while generating braintree client token", e,
                    FunctionCodes.BrainTreeClientTokenGeneration);
            }
        }

        public async Task<string> CreateSubscriptionAsync(string customerId, string paymentMethodNonce)
        {
            var paymentMethodRequest = new PaymentMethodRequest
            {
                CustomerId = customerId,
                PaymentMethodNonce = paymentMethodNonce
            };

            Result<PaymentMethod> paymentMethodResult = await _gateway.PaymentMethod.CreateAsync(paymentMethodRequest);

            if (!paymentMethodResult.IsSuccess())
            {
                _logger.Log(LogLevel.Error, $"{paymentMethodResult.Message}");
                
                throw new IBSDiscountCardApiErrorException(FunctionCodes.BraintreePaymentMethodCreation);
            }

            var subscriptionRequest = new SubscriptionRequest
            {
                PaymentMethodToken = paymentMethodResult.Target.Token,
                PlanId = _options.PlanId,
                MerchantAccountId = _options.MerchantAccountId
            };

            Result<Subscription> subscriptionResult = await _gateway.Subscription.CreateAsync(subscriptionRequest);

            if (!subscriptionResult.IsSuccess())
            {
                _logger.Log(LogLevel.Error, $"{subscriptionResult.Message}");
                
                throw new IBSDiscountCardApiErrorException(FunctionCodes.BraintreeSubscriptionCreation);
            }

            return subscriptionResult.Target.Id;
        }

        public async Task HandleWebhookAsync(string signature, string payload)
        {
            WebhookNotification webhookNotification = _gateway.WebhookNotification.Parse(
                signature, payload
            );

            if (webhookNotification.Kind.ToString() == WebhookKind.SUBSCRIPTION_CANCELED.ToString())
            {
                _logger.LogInformation($"Received SUBSCRIPTION_CANCELED webhook and cancel subscription",
                    webhookNotification.Subscription);

                await CancelAsync(webhookNotification.Subscription);
            }
            else if (webhookNotification.Kind.ToString() == WebhookKind.SUBSCRIPTION_EXPIRED.ToString())
            {
                _logger.LogInformation($"Received SUBSCRIPTION_EXPIRED webhook and cancel subscription",
                    webhookNotification.Subscription);

                await CancelAsync(webhookNotification.Subscription);
            }
            else if (webhookNotification.Kind.ToString() == WebhookKind.SUB_MERCHANT_ACCOUNT_DECLINED.ToString())
            {
                _logger.LogInformation($"Received SUB_MERCHANT_ACCOUNT_DECLINED webhook and cancel subscription",
                    webhookNotification.Subscription);

                await CancelAsync(webhookNotification.Subscription);
            }
            else
            {
                _logger.LogInformation($"Received webhook but not handled");
            }

            async Task CancelAsync(Subscription subscription)
            {
                var user = await _userRepository.GetAsync(new ExternalSubscriptionId(subscription.Id));

                user.CancelPremiumSubscription(_dateTimeHelper.Now);

                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task CancelSubscriptionAsync(string subscriptionId)
        {
            var result = await _gateway.Subscription.CancelAsync(subscriptionId);

            if (!result.IsSuccess())
            {
                _logger.Log(LogLevel.Error, $"{result.Message}");
                
                throw new IBSDiscountCardApiErrorException(FunctionCodes.BraintreeSubscriptionCancel);
            }
        }
    }
}