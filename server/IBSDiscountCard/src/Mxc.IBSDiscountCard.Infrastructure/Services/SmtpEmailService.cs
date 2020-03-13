using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public SmtpEmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task SendEmailAsync(User addressee, string content, string contentHTML = "", string subject = "")
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(addressee.FullName, addressee.Email));
            message.From.Add(new MailboxAddress(_emailOptions.SenderFullName, _emailOptions.SenderEmail));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder()
            {
                HtmlBody = $"{contentHTML}",
                TextBody = $"{content}",
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_emailOptions.EmailServiceUrl, 587);
                client.Authenticate(_emailOptions.EmailServiceUser, _emailOptions.EmailServicePassword);
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
