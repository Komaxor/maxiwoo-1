
using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class RegisterUser : IIBSDiscountCardCommand<string>
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public RegisterUser(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
        }
    }
}
