using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mxc.IBSDiscountCard.Common;

namespace Mxc.IBSDiscountCard.WebApi.Controllers
{
    /// <summary>
    /// Base class for every controller
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = Roles.AllRoles)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiControllerBase : Mxc.WebApi.Abstractions.Controller.ApiControllerBase
    {
    }
}
