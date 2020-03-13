using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Responses;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class ChangeMyPasswordHandler : IBSDiscountCardCommandHandler<ChangeMyPassword, object>
    {
        private readonly ILoggedInUserAccessor _loggedInUser;
        private readonly ILoginManager _loginManager;

        public ChangeMyPasswordHandler(ILogger<ChangeMyPasswordHandler> logger, ILoggedInUserAccessor loggedInUser, ILoginManager loginManager) : base(logger)
        {
            _loggedInUser = loggedInUser;
            _loginManager = loginManager;
        }

        public override async Task<IExecutionResult<object, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(ChangeMyPassword command, CancellationToken cancellationToken)
        {
            await _loginManager.ChangePasswordAsync(_loggedInUser.UserName, command.OldPassword, command.NewPassword);

            return IBSDiscountCardExecutionResult<object>.FromResult(new object());
        }
    }
}
