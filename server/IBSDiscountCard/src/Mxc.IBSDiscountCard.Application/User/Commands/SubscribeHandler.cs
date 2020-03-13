using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mxc.Helpers.DateTime;
using Mxc.IBSDiscountCard.Application.User.Services;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class SubscribeHandler : IBSDiscountCardCommandHandler<Subscribe, object>
    {
        private readonly ILoggedInUserAccessor _loggedInUser;
        private readonly IPaymentGateway _paymentGateway;
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeHelper _dateTimeHelper;

        public SubscribeHandler(ILoggedInUserAccessor loggedInUser, 
            IPaymentGateway paymentGateway,
            IUserRepository userRepository, 
            IDateTimeHelper dateTimeHelper,
            ILogger<SubscribeHandler> logger) : base(logger)
        {
            _loggedInUser = loggedInUser;
            _paymentGateway = paymentGateway;
            _userRepository = userRepository;
            _dateTimeHelper = dateTimeHelper;
        }

        public override async Task<IExecutionResult<object, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(Subscribe command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(new UserNameEquals(_loggedInUser.UserName));
            if (user == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.SubscribeUserNotFound);
            }

            if (!user.Subscription.CanUpgradeToPremium())
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.UserAlreadyHasPremiumSubscription);
            }
            
            try
            {
                var subscription = await _paymentGateway.CreateSubscriptionAsync(user.CustomerId, command.Nonce);
                
                user.SubscribeToPremium(_dateTimeHelper.Now, subscription);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error during subscription creation");
                throw new IBSDiscountCardApiErrorException(FunctionCodes.SubscribeErrorFromProvider);
            }
            

            var isSuccess = await _userRepository.UpdateAsync(user);
            
            if (!isSuccess)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.SubscribeUpdateNotFullfiled);
            }

            return IBSDiscountCardExecutionResult<object>.FromResult(new object());
        }
    }
}
