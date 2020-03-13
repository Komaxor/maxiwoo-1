using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mxc.IBSDiscountCard.WebApi
{
    public class AuthorizationSwaggerOperationFilter: IOperationFilter
    {
        [System.Obsolete]
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.ApiDescription.ControllerAttributes().OfType<AuthorizeAttribute>().Any() ||
                               context.ApiDescription.ActionAttributes().OfType<AuthorizeAttribute>().Any();
            
            if (hasAuthorize)
            {
                operation.Responses.TryAdd("401", new Response { Description = "Unauthorized" });
                operation.Responses.TryAdd("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                operation.Security.Add(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new []{""} }
                });
            }
        }
    }
}