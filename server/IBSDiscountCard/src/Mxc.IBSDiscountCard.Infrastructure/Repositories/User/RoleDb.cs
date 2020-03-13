using System;
using Microsoft.AspNetCore.Identity;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.User
{
    public class RoleDb : IdentityRole<Guid>
    {
        public RoleDb()
        {
        }

        public RoleDb(string roleName)
        {
            Name = roleName;
        }
    }
}