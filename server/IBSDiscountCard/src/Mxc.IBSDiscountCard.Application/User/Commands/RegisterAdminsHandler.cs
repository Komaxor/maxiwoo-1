using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mxc.Commands.Abstractions.Commands;
using Mxc.Helpers.DateTime;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Queries;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class RegisterAdminsHandler : IBSDiscountCardCommandHandler<RegisterAdmins, object>
    {
        private readonly ILoginManager _loginManager;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly InitAdminOptions _options;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentGateway _paymentGateway;

        public RegisterAdminsHandler(
            ILoginManager loginManager,
            IOptions<InitAdminOptions> options,
            IDateTimeHelper dateTimeHelper,
            IUserRepository userRepository,
            IPaymentGateway paymentGateway,
            ILogger<RegisterAdminsHandler> logger) : base(logger)
        {
            _loginManager = loginManager;
            _dateTimeHelper = dateTimeHelper;
            _options = options.Value;
            _userRepository = userRepository;
            _paymentGateway = paymentGateway;
        }

        public override async Task<IExecutionResult<object, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(RegisterAdmins command, CancellationToken cancellationToken)
        {
            foreach (var optionsUser in _options.Users)
            {
                var existingUser = await _userRepository.GetAsync(new UserNameEquals(optionsUser.Email));

                if (existingUser == null)
                {
                    var customerId = await _paymentGateway.CreateCustomerAsync(optionsUser.FullName, optionsUser.Email);
                    
                    var user = new Domain.UserAggregate.User(
                        optionsUser.FullName,
                        optionsUser.Email,
                        optionsUser.Password,
                        "",
                        new InstituteId(MockData.InstitudeId),
                        _dateTimeHelper.Now,
                        customerId
                    );
                    user.Activate();

                    await _loginManager.RegistrationAsync(user, Roles.Admin);
                }
            }

            return IBSDiscountCardExecutionResult<object>.FromResult(new object());
        }
    }
}