using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class LoginUser : IIBSDiscountCardCommand<LoginUserResponse>
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public LoginUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
