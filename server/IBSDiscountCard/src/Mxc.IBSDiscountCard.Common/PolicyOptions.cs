using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Common
{
    public class PolicyOptions
    {
        public int PasswordMinLenght { get; set; }
        public int ActivationCodeLenght { get; set; }
        public int PasswordResetCodeLenght { get; set; }
        public int PasswordChangeMaxWrongTry { get; set; }
        public int PasswordChangeLockEndInMinutes { get; set; }
        public int PasswordResetLockEndInMinutes { get; set; }
        public string CodeCharacters { get; set; }
    }
}
