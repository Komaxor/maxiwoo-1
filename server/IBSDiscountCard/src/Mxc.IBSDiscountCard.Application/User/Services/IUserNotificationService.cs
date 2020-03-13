using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.User.Services
{
    public interface IUserNotificationService
    {
        Task SendActivationEmailAsync(Domain.UserAggregate.User addressee, string code);

        Task SendPasswordResetEmailAsync(Domain.UserAggregate.User addressee, string code);
    }
}
