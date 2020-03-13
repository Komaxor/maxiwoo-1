using AutoMapper;
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

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class ActivateUserHandler : IBSDiscountCardCommandHandler<ActivateUser, object>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ActivateUserHandler> _logger;
        private readonly ILoggedInUserAccessor _loggedInUser;

        public ActivateUserHandler(IUserRepository userRepository, ILoggedInUserAccessor loggedInUser, ILogger<ActivateUserHandler> logger) : base(logger)
        {
            _userRepository = userRepository;
            _logger = logger;
            _loggedInUser = loggedInUser;
        }

        public override async Task<IExecutionResult<object, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(ActivateUser command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(new UserNameEquals(_loggedInUser.UserName));
            if (user == null)
            {
                return IBSDiscountCardExecutionResult<object>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.ActivateUserUserNotFound));
            }

            if (user.EmailConfirmed)
            {
                return IBSDiscountCardExecutionResult<object>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.ActivateUserAlreadyConfirmed));
            }

            if (user.ActivationCode != command.ActivationCode.Trim().ToUpper())
            {
                return IBSDiscountCardExecutionResult<object>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.ActivateUserWrongCode));
            }

            user.Activate();

            var isSuccess = await _userRepository.UpdateAsync(user);

            if (!isSuccess)
            {
                _logger.LogError("Error during user activation, the database operation not fullfiled.");
                return IBSDiscountCardExecutionResult<object>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.ActivateUserUpdateNotFullfiled));
            }

            return IBSDiscountCardExecutionResult<object>.FromResult(new object());
        }
    }
}
