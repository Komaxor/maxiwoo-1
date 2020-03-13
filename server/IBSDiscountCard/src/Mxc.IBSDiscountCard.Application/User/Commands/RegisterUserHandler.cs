using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mxc.Helpers.DateTime;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class RegisterUserHandler : IBSDiscountCardCommandHandler<RegisterUser, string>
    {
        private readonly ILoginManager _loginManager;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IUserNotificationService _notificationService;
        private readonly IPaymentGateway _paymentGateway;

        private readonly PolicyOptions _policyOptions;

        public RegisterUserHandler(ILoginManager loginManager, IDateTimeHelper dateTimeHelper,
            IUserNotificationService notificationService,
            IPaymentGateway paymentGateway,
            IOptions<PolicyOptions> options, ILogger<RegisterUserHandler> logger) : base(logger)
        {
            _loginManager = loginManager;
            _dateTimeHelper = dateTimeHelper;
            _policyOptions = options.Value;
            _notificationService = notificationService;
            _paymentGateway = paymentGateway;
        }

        public override async Task<IExecutionResult<string, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(
            RegisterUser command, CancellationToken cancellationToken)
        {
            var customerId = await _paymentGateway.CreateCustomerAsync(command.FullName, command.Email);

            var user = new Domain.UserAggregate.User(
                command.FullName,
                command.Email,
                command.Password,
                "",
                new InstituteId(MockData.InstitudeId),
                _dateTimeHelper.Now,
                customerId
            );

            var code = _loginManager.GenerateCode(_policyOptions.ActivationCodeLenght);
            user.ModifyActivationCode(code);

            await _notificationService.SendActivationEmailAsync(user, code);

            await _loginManager.RegistrationAsync(user);

            return IBSDiscountCardExecutionResult<string>.FromResult("Successful registration");
        }
    }
}