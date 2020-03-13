using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Services;
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

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class UnsubscribeHandler : IBSDiscountCardCommandHandler<Unsubscribe, object>
    {
        private readonly ILoggedInUserAccessor _loggedInUser;
        private readonly IUserRepository _userRepository;
        private readonly ILoginManager _loginManager;
        private readonly IPaymentGateway _paymentGateway;
        private readonly IDateTimeHelper _dateTimeHelper;

        public UnsubscribeHandler(ILoggedInUserAccessor loggedInUser,
            IUserRepository userRepository,
            ILoginManager loginManager,
            IPaymentGateway paymentGateway,
            IDateTimeHelper dateTimeHelper,
            ILogger<UnsubscribeHandler> logger) : base(logger)
        {
            _loggedInUser = loggedInUser;
            _userRepository = userRepository;
            _loginManager = loginManager;
            _paymentGateway = paymentGateway;
            _dateTimeHelper = dateTimeHelper;
        }

        public override async Task<IExecutionResult<object, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(
            Unsubscribe command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(new UserNameEquals(_loggedInUser.UserName));
            if (user == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.UnsubscribeUserNotFound);
            }

            if (_loggedInUser.UserName != command.Email ||
                !await _loginManager.VerifyPasswordAsync(user, command.Password))
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.UnsubscribeWrongUserInput);
            }

            if (!user.Subscription.CanCancelPremium())
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.UnsubscribeUserIsNotPremium);
            }

            try
            {
                await _paymentGateway.CancelSubscriptionAsync(user.Subscription.ExternalSubscriptionId);

                user.CancelPremiumSubscription(_dateTimeHelper.Now);
            }
            catch (Exception)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.UnsubscribeErrorFromProvider);
            }

            var isSuccess = await _userRepository.UpdateAsync(user);

            if (!isSuccess)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.UnsubscribeUpdateNotFullfiled);
            }

            return IBSDiscountCardExecutionResult<object>.FromResult(new object());
        }
    }
}