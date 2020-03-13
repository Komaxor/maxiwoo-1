using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class Unsubscribe : IIBSDiscountCardCommand<object>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Unsubscribe(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
