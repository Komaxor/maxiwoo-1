using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Common
{
    public class JWTOption
    {
        public string SingKey { get; set; }
        public int ExpireDays { get; set; }
        public string TokenType { get; set; }
    }
}
