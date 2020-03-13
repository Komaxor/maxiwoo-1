using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.User;

namespace Mxc.IBSDiscountCard.WebApi
{
    public class IbsClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserDb>{
        public IbsClaimsPrincipalFactory(UserManager<UserDb> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserDb user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(IBSClaimTypes.InstituteId, user.InstitudeId.ToString(),
                ClaimValueTypes.String));
            return identity;
        }
    }
}