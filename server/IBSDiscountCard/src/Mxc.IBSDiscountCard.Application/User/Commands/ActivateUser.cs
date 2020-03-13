using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class ActivateUser : IIBSDiscountCardCommand<object>
    {
        public string ActivationCode { get; private set; }

        public ActivateUser(string activationCode)
        {
            ActivationCode = activationCode;
        }
    }
}
