using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class SetNewPassword : IIBSDiscountCardCommand<object>
    {
        public string Code { get; private set; }
        public string NewPassword { get; private set; }

        public SetNewPassword(string code, string newPassword)
        {
            Code = code;
            NewPassword = newPassword;
        }
    }
}
