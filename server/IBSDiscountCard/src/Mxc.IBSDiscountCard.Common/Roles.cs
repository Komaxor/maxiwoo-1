using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Common
{
    /// <summary>
    /// ASP Identity roles
    /// </summary>
    public class Roles
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string AllRoles = Admin + "," + Customer;
    }
}
