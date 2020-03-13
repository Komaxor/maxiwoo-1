using Microsoft.Extensions.Configuration;
using Mxc.IBSDiscountCard.WebApi;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace Mxc.IBSDiscountCard.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }
        protected override List<string> CollectDocumentations()
        {
            return new List<string>();
        }
    }
}
