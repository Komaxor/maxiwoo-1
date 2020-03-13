using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Responses;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class LoginUserHandler : IBSDiscountCardCommandHandler<LoginUser, LoginUserResponse>
    {
        private readonly ILoginManager _loginManager;
        private readonly JWTOption _jwtOptions;

        public LoginUserHandler(ILoginManager loginManager, IOptions<JWTOption> jwtOptions, ILogger<LoginUserHandler> logger) : base(logger)
        {
            _loginManager = loginManager;
            _jwtOptions = jwtOptions.Value;
        }

        public override async Task<IExecutionResult<LoginUserResponse, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(LoginUser command, CancellationToken cancellationToken)
        {
            var token = await _loginManager.LoginAsync(command.UserName, command.Password);

            return IBSDiscountCardExecutionResult<LoginUserResponse>.FromResult(new LoginUserResponse()
            {
                Access_token = token,
                Expires_in = DateTimeOffset.Now.AddDays(_jwtOptions.ExpireDays).ToUnixTimeSeconds() - DateTimeOffset.Now.ToUnixTimeSeconds(),
                Token_type = _jwtOptions.TokenType
            });
        }
    }
}
