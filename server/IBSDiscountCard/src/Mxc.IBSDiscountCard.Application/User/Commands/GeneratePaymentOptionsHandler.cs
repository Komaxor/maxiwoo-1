using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class
        GeneratePaymentOptionsHandler : IBSDiscountCardCommandHandler<GeneratePaymentOptions,
            GeneratePaymentOptionsResponse>
    {
        private readonly ILoggedInUserAccessor _loggedInUser;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentGateway _paymentGateway;
        private readonly PaymentOptions _paymentOptions;

        public GeneratePaymentOptionsHandler(ILoggedInUserAccessor loggedInUser,
            IUserRepository userRepository,
            IPaymentGateway paymentGateway,
            IOptions<PaymentOptions> paymentOptions,
            ILogger<GeneratePaymentOptionsHandler> logger) : base(logger)
        {
            _loggedInUser = loggedInUser;
            _userRepository = userRepository;
            _paymentGateway = paymentGateway;
            _paymentOptions = paymentOptions.Value;
        }

        public override async
            Task<IExecutionResult<GeneratePaymentOptionsResponse, FunctionCodes, IBSDiscountCardExecutionError>>
            HandleAsync(GeneratePaymentOptions command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(new UserNameEquals(_loggedInUser.UserName));
            if (user == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.SubscribeUserNotFound);
            }

            var clientToken = await _paymentGateway.GenerateClientTokenAsync(user.CustomerId);

            return IBSDiscountCardExecutionResult<GeneratePaymentOptionsResponse>.FromResult(
                new GeneratePaymentOptionsResponse()
                {
                    ClientToken = clientToken,
                    CountryCode = _paymentOptions.CountryCode,
                    CurrencyCode = _paymentOptions.CurrencyCode,
                    MerchantIdentifier = _paymentOptions.MerchantId,
                    MerchantName = _paymentOptions.MerchantName
                });
        }
    }
}