using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.AspNetCore.TestBase;
using Xunit;

namespace HelloWorld.Web.Tests
{
    public class ExceptionControllerTests : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        [Fact]
        public async Task ShouldReceiveException()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                @"api/exception",
                HttpStatusCode.InternalServerError);

            // Assert
            Assert.NotNull(response);
        }
    }
}
