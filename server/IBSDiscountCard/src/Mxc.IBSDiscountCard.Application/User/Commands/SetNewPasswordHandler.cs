using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class SetNewPasswordHandler : IBSDiscountCardCommandHandler<SetNewPassword, object>
    {
        private readonly ILoginManager _loginManager;

        public SetNewPasswordHandler(ILoginManager loginManager, ILogger<SetNewPasswordHandler> logger) : base(logger)
        {
            _loginManager = loginManager;
        }

        public override async Task<IExecutionResult<object, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(SetNewPassword command, CancellationToken cancellationToken)
        {
            if (!await _loginManager.ResetPasswordAsync(command.NewPassword, command.Code))
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.SetNewPasswordUpdateNotFullfiled);
            }

            return IBSDiscountCardExecutionResult<object>.FromResult(new object());
        }
    }
}
