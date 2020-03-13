using Mxc.IBSDiscountCard.Application.User.Commands;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.WebApi.SwaggerExamples
{
    public class ChangeMyPasswordCommandExample : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (context.SystemType == typeof(ChangeMyPassword))
            {
                schema.Example = new ChangeMyPassword("Qwer1234", "Qwer5678");
            }
        }
    }
}
