using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Xunit;

namespace HelloWorld.Web.Tests
{
    public class HelloControllerTests : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        [Fact]
        public async Task ShouldHelloInPortugueseCulture()
        {
            var culture = CultureInfo.GetCultureInfo("pt-BR");

            // Arrange
            SetRequestCulture(culture);

            // Act
            var response = await GetResponseAsObjectAsync<string>(
                @"api/hello",
                HttpStatusCode.OK);

            // Assert
            var localizedHelloMessage = GetLocalizedString(Constants.LocalizationSourceName, GlobalizationKey.Hello, culture);

            Assert.Equal(localizedHelloMessage, response);
        }

        [Fact]
        public async Task ShouldHelloInEnglishCulture()
        {
            var culture = CultureInfo.GetCultureInfo("en");

            // Arrange
            SetRequestCulture(culture);

            // Act
            var response = await GetResponseAsObjectAsync<string>(
                @"api/hello",
                HttpStatusCode.OK);

            // Assert
            var localizedHelloMessage = GetLocalizedString(Constants.LocalizationSourceName, GlobalizationKey.Hello, culture);

            Assert.Equal(localizedHelloMessage, response);
        }
    }
}
