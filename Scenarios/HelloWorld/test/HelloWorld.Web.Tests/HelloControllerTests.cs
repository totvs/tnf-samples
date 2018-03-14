using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Tnf.Localization;
using Xunit;

namespace HelloWorld.Web.Tests
{
    public class HelloControllerTests : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        private ILocalizationManager localizationManager;
        private CultureInfo requestCulture;

        private void SetRequestCulture(CultureInfo cultureInfo)
        {
            Client.DefaultRequestHeaders.Add(CookieRequestCultureProvider.DefaultCookieName, $"c={cultureInfo.Name}|uic={cultureInfo.Name}");

            localizationManager = ServiceProvider.GetService<ILocalizationManager>();

            requestCulture = cultureInfo;
        }

        [Fact]
        public async Task ShouldHelloInPortugueseCulture()
        {
            // Arrange
            SetRequestCulture(CultureInfo.GetCultureInfo("pt-BR"));

            // Act
            var response = await GetResponseAsObjectAsync<string>(
                @"api/hello",
                HttpStatusCode.OK);

            // Assert
            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            var localizedHelloMessage = localizationSource.GetString(GlobalizationKey.Hello, requestCulture);

            Assert.Equal(localizedHelloMessage, response);
        }

        [Fact]
        public async Task ShouldHelloInEnglishCulture()
        {
            // Arrange
            SetRequestCulture(CultureInfo.GetCultureInfo("en"));

            // Act
            var response = await GetResponseAsObjectAsync<string>(
                @"api/hello",
                HttpStatusCode.OK);

            // Assert
            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            var localizedHelloMessage = localizationSource.GetString(GlobalizationKey.Hello, requestCulture);

            Assert.Equal(localizedHelloMessage, response);
        }
    }
}
