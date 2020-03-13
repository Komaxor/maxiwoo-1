using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Responses
{
    public class LoginUserResponse
    {
        public string Access_token { get; set; }
        public long Expires_in { get; set; }
        public string Token_type { get; set; }
    }
}
