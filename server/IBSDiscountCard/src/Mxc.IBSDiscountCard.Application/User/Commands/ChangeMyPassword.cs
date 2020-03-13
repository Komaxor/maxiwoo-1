using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.User.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class ChangeMyPassword : IIBSDiscountCardCommand<object>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangeMyPassword(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
