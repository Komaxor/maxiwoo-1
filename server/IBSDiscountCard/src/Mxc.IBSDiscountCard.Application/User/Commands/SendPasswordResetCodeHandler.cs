using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mxc.Commands.Abstractions.Commands;
using Mxc.Helpers.Errors;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class SendPasswordResetCodeHandler : IBSDiscountCardCommandHandler<SendPasswordResetCode, object>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginManager _loginManager;
        private readonly IUserNotificationService _notificationService;
        private readonly PolicyOptions _policyOptions;
        private readonly ILogger<SendPasswordResetCodeHandler> _logger;

        public SendPasswordResetCodeHandler(IUserRepository userRepository, ILoginManager loginManager, IUserNotificationService notificationService, IOptions<PolicyOptions> options, ILogger<SendPasswordResetCodeHandler> logger) : base(logger)
        {
            _userRepository = userRepository;
            _loginManager = loginManager;
            _policyOptions = options.Value;
            _notificationService = notificationService;
            _logger = logger;
        }

        public override async Task<IExecutionResult<object, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(SendPasswordResetCode command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(new UserNameEquals(command.Email));
            if (user == null)
            {
                _logger.LogInformation("User password reset code request refused because the following email '{0}' not exists in the system.", command.Email);
                return IBSDiscountCardExecutionResult<object>.FromResult(new object());
            }

            var code = _loginManager.GenerateCode(_policyOptions.PasswordResetCodeLenght);

            try
            {
                if (!await _loginManager.SavePasswordResetCodeAsync(user, code))
                {
                    throw new IBSDiscountCardApiErrorException(FunctionCodes.PasswordResetUpdateNotFullfiled);
                }

                await _notificationService.SendPasswordResetEmailAsync(user, code);

                return IBSDiscountCardExecutionResult<object>.FromResult(new object());
            }
            catch (ApiErrorException e) when (e.Error.FunctionCode == FunctionCodes.PasswordResetEmailSendingError.ToString())
            {
                if (!await _loginManager.RemovePasswordResetCodeAsync(user))
                {
                    throw new IBSDiscountCardApiErrorException(FunctionCodes.PasswordResetClearUpdateNotFullfiled);
                }

                throw;
            }
            catch (ApiErrorException e) when (e.Error.FunctionCode == FunctionCodes.PasswordResetDisabledForaWhile.ToString())
            {
                return IBSDiscountCardExecutionResult<object>.FromResult(new object());
            }
        }
    }
}
