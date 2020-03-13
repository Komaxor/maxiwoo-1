using System;
using System.Collections.Generic;
using Mxc.IBSDiscountCard.Application.Place.Commands;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mxc.IBSDiscountCard.WebApi.SwaggerExamples
{
    public class AddPlaceCommandExample : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (context.SystemType == typeof(AddPlace))
            {
                schema.Example =
                    new AddPlace("Teszt hely",
                        "Restaurants",
                        "about us",
                        null,
                        new AddressDto("1139 Budapest, Nagy Király utca 2", "47.549246", "19.072631"),
                        new string("tag1, tag2"),
                        new OpeningHoursOfDayDto("7-10", "7-10", "7-10", "7-10", "7-10", "7-10", "7-10"),
                        false, 1, "9999990", "test@gmail.com", "https://stackoverflow.com/", "https://stackoverflow.com/", "https://stackoverflow.com/");
            }
        }
    }
}