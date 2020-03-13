using Mxc.IBSDiscountCard.Application.User.Commands;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.WebApi.SwaggerExamples
{
    public class LoginUserCommandExample : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (context.SystemType == typeof(LoginUser))
            {
                schema.Example = new LoginUser("test@ibs-b.hu", "Qwer1234");
            }
        }
    }
}
