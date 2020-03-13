using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Common
{
    public class EmailOptions
    {
        public string EmailServiceUser { get; set; }
        public string EmailServicePassword { get; set; }
        public string EmailServiceUrl { get; set; }
        public string SenderEmail { get; set; }
        public string SenderFullName { get; set; }
        public string ActivationEmailSubject { get; set; }
        public string ActivationEmailContent { get; set; }
        public string PasswordResetEmailSubject { get; set; }
        public string PasswordResetEmailContent { get; set; }
    }
}
