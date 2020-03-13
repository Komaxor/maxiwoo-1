using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.User.Services
{
    public interface ILoginManager
    {
        Task RegistrationAsync(Domain.UserAggregate.User user, string role = Roles.Customer);
        Task<string> LoginAsync(string username, string password);
        string GenerateCode(int lenght);
        Task ChangePasswordAsync(string username, string oldpassword, string newpassword);
        Task<bool> SavePasswordResetCodeAsync(Domain.UserAggregate.User user, string code);
        Task<bool> RemovePasswordResetCodeAsync(Domain.UserAggregate.User user);
        Task<bool> ResetPasswordAsync(string newpassword, string code);
        Task<bool> VerifyPasswordAsync(Domain.UserAggregate.User user, string password);
        Task<bool> HasRoleAsync(string userName, string role);
    }
}
