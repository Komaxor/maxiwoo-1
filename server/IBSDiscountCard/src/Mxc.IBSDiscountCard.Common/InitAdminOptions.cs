using System.Collections.Generic;

namespace Mxc.IBSDiscountCard.Common
{
    public class InitAdminOptions
    {
        public List<User> Users { get; set; }

        public class User
        {
            public string Email { get; set; }

            public string Password { get; set; }

            public string FullName { get; set; }
        }
    }
}