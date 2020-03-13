using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class SendPasswordResetCode : IIBSDiscountCardCommand<object>
    {
        public string Email { get; private set; }

        public SendPasswordResetCode(string email)
        {
            Email = email;
        }
    }
}
