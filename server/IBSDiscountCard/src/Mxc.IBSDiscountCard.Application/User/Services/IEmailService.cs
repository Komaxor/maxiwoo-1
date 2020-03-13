using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.User.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Domain.UserAggregate.User addressee, string content, string contentHTML = "", string subject = "");
    }
}
