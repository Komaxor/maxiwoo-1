using Mxc.IBSDiscountCard.Application.Image.Commands;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.WebApi.SwaggerExamples
{
    public class UploadPlaceImageCommandExample : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (context.SystemType == typeof(UploadPlaceImage))
            {
                schema.Example = new UploadPlaceImage(Guid.Parse("e5906014-cc56-4aff-abb3-2db3cdb40a21"), null);
            }
        }
    }
}
