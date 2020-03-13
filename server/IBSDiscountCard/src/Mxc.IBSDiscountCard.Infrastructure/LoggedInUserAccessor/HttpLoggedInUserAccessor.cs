using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;

namespace Mxc.IBSDiscountCard.Infrastructure.LoggedInUserAccessor
{
    /// <summary>
    /// Logged in user implementation to get data from HTTP request
    /// </summary>
    public class HttpLoggedInUserAccessor : ILoggedInUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Guid InstitueId => Guid.Parse(GetUserClaimValue(IBSClaimTypes.InstituteId, Guid.Empty.ToString()));
        //JwtRegisteredClaimNames.Sub helyett az ms specifikus kell
        public string UserName => GetUserClaimValue(ClaimTypes.Name);

        public HttpLoggedInUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserClaimValue(string claimType, string defaultValue = null)
        {
            if (_httpContextAccessor?.HttpContext?.User?.Claims?.Any(c => c.Type == claimType) == true)
            {
                return _httpContextAccessor?.HttpContext?.User?.Claims?.First(c => c.Type == claimType).Value;
            }

            return defaultValue;
        }
    }
}