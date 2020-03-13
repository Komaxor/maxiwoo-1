using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Infrastructure.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IEmailService _emailService;
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<UserNotificationService> _logger;

        public UserNotificationService(IEmailService emailService, IOptions<EmailOptions> options, ILogger<UserNotificationService> logger)
        {
            _emailService = emailService;
            _emailOptions = options.Value;
            _logger = logger;
        }

        public async Task SendActivationEmailAsync(Domain.UserAggregate.User addressee, string code)
        {
            var content = $"{_emailOptions.ActivationEmailContent}: {code}";
            var contenthtml = $"{_emailOptions.ActivationEmailContent}: <b>{code}</b>";

            try
            {
                await _emailService.SendEmailAsync(addressee, content, contenthtml, _emailOptions.ActivationEmailSubject);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during activation email sending.");
                throw new IBSDiscountCardApiErrorException(FunctionCodes.SendActivateEmailSendingError);
            }
        }

        public async Task SendPasswordResetEmailAsync(Domain.UserAggregate.User addressee, string code)
        {
            var content = $"{_emailOptions.PasswordResetEmailContent}: {code}";
            var contenthtml = $"{_emailOptions.PasswordResetEmailContent}: <b>{code}</b>";

            try
            {
                await _emailService.SendEmailAsync(addressee, content, contenthtml, _emailOptions.PasswordResetEmailSubject);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during password reset email sending.");
                throw new IBSDiscountCardApiErrorException(FunctionCodes.PasswordResetEmailSendingError);
            }
        }
    }
}
