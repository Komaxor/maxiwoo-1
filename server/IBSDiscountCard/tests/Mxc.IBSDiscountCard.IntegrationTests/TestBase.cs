using Mxc.Test.Abstractions.TestBase;
using Mxc.Test.Abstractions.WebApplicationFactory;
using Xunit.Abstractions;

namespace Mxc.IBSDiscountCard.IntegrationTests
{
    public class TestBase : Mxc.Test.Abstractions.TestBase.TestBase<TestStartup>
    {
        public TestBase(TestWebApplicationFactory<TestStartup> webApplicationFactory, ITestOutputHelper testOutputHelper) : base(webApplicationFactory, testOutputHelper)
        {
        }
    }
}
