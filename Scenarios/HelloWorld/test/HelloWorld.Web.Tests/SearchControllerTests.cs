using HelloWorld.SharedKernel;
using HelloWorld.SharedKernel.External;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.AspNetCore.TestBase;
using Tnf.Localization;
using Xunit;

namespace HelloWorld.Web.Tests
{
    public class SearchControllerTests : TnfAspNetCoreIntegratedTestBase<StartupTest>
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
        public async Task ShouldSearchZipCodeWithSucess()
        {
            SetRequestCulture(CultureInfo.GetCultureInfo("pt-BR"));

            // Act
            var response = await GetResponseAsObjectAsync<SearchLocationResponse>(
                @"api/search/11111111",
                HttpStatusCode.OK);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("São Paulo", response.City);
            Assert.Equal("SP", response.State);
            Assert.Equal("Praça da Sé", response.Street);
            Assert.Equal("11111111", response.ZipCode.Number);
        }

        [Fact]
        public async Task ShouldSearchZipCodeWhenNotFound()
        {
            SetRequestCulture(CultureInfo.GetCultureInfo("pt-BR"));

            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                @"api/search/12345678",
                HttpStatusCode.NotFound);

            // Assert
            Assert.NotNull(response);

            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            foreach (var detail in response.Details)
            {
                Assert.Equal(
                    LocalizationServiceError.LocalizationInvalidZipCode.ToString(),
                    detail.DetailedMessage);

                var localizedMessage = localizationSource.GetString(LocalizationServiceError.LocalizationInvalidZipCode, requestCulture);

                Assert.Equal(localizedMessage, detail.Message);
            }
        }

        [Fact]
        public async Task ShouldSearchCompleteAddressWithSucess()
        {
            SetRequestCulture(CultureInfo.GetCultureInfo("pt-BR"));

            // Act
            var response = await GetResponseAsObjectAsync<List<SearchLocationResponse>>(
                @"api/search/RS/Porto%20Alegre/Rua%20São",
                HttpStatusCode.OK);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Count);
        }

        [Fact]
        public async Task ShouldSearchCompleteAddressWhenNotFound()
        {
            SetRequestCulture(CultureInfo.GetCultureInfo("pt-BR"));

            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                @"api/search/RS/Porto%20Alegre/a",
                HttpStatusCode.NotFound);

            // Assert
            Assert.NotNull(response);

            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            foreach (var detail in response.Details)
            {
                Assert.Equal(
                    LocalizationServiceError.LocalizationInvalidCityOrStreet.ToString(),
                    detail.DetailedMessage);

                var localizedMessage = localizationSource.GetString(LocalizationServiceError.LocalizationInvalidCityOrStreet, requestCulture);

                Assert.Equal(localizedMessage, detail.Message);
            }
        }

        [Fact]
        public async Task SearchZipCodeWithInvalidParameterWithCustomCulture()
        {
            SetRequestCulture(CultureInfo.GetCultureInfo("en"));

            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                @"api/search/%20",
                HttpStatusCode.BadRequest);

            // Assert
            Assert.NotNull(response);

            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            foreach (var detail in response.Details)
            {
                Assert.Equal(
                    LocalizationServiceError.LocalizationInvalidZipCode.ToString(),
                    detail.DetailedMessage);

                var localizedMessage = localizationSource.GetString(LocalizationServiceError.LocalizationInvalidZipCode, requestCulture);

                Assert.Equal(localizedMessage, detail.Message);
            }
        }
    }
}
