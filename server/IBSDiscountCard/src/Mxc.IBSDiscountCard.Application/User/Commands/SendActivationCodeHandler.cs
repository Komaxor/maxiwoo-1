using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class SendActivationCodeHandler : IBSDiscountCardCommandHandler<SendActivationCode, object>
    {
        private readonly ILoginManager _loginManager;
        private readonly IUserRepository _userRepository;
        private readonly IUserNotificationService _notificationService;
        private readonly ILogger<SendActivationCodeHandler> _logger;
        private readonly ILoggedInUserAccessor _loggedInUser;
        private readonly PolicyOptions _policyOptions;

        public SendActivationCodeHandler(ILoginManager loginManager, IUserRepository userRepository, IUserNotificationService notificationService, ILoggedInUserAccessor loggedInUser,
            IOptions<PolicyOptions> options,
            ILogger<SendActivationCodeHandler> logger)
            : base(logger)
        {
            _loginManager = loginManager;
            _userRepository = userRepository;
            _logger = logger;
            _loggedInUser = loggedInUser;
            _policyOptions = options.Value;
            _notificationService = notificationService;
        }

        public override async Task<IExecutionResult<object, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(SendActivationCode command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(new UserNameEquals(_loggedInUser.UserName));
            if (user == null)
            {
                return IBSDiscountCardExecutionResult<object>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.SendActivateCodeUserUserNotFound));
            }

            if (user.EmailConfirmed)
            {
                return IBSDiscountCardExecutionResult<object>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.SendActivateCodeAlreadyConfirmed));
            }

            var code = _loginManager.GenerateCode(_policyOptions.ActivationCodeLenght);
            user.ModifyActivationCode(code);

            await _notificationService.SendActivationEmailAsync(user, code);

            var isSuccess = await _userRepository.UpdateAsync(user);
            if (!isSuccess)
            {
                _logger.LogError("Error during user Activation, email has been sent but the database operation not fullfiled.");
                return IBSDiscountCardExecutionResult<object>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.SendActivateCodeUpdateNotFullfiled));
            }

            return IBSDiscountCardExecutionResult<object>.FromResult(new object());
        }
    }
}
