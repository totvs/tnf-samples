using Case1.Domain;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Tnf.Localization;
using Tnf.Notifications;
using Xunit;

namespace Case1.Web.Tests
{
    public class HashControllerTests : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly CultureInfo _culture;

        public HashControllerTests()
        {
            Client.DefaultRequestHeaders.Add(CookieRequestCultureProvider.DefaultCookieName, "c=pt-BR|uic=pt-BR");

            _localizationManager = ServiceProvider.GetService<ILocalizationManager>();

            _culture = CultureInfo.GetCultureInfo("pt-BR");
        }

        [Fact]
        public async Task Should_Return_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<string>(
                @"api/hash?value=valor",
                HttpStatusCode.OK);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Return_With_Notifications()
        {
            // Act
            var response = await GetResponseAsObjectAsync<List<NotificationEvent>>(
                @"api/hash",
                HttpStatusCode.BadRequest);

            var notificatioEvent = _localizationManager
                .GetSource(Case1Consts.LocalizationSourceName)
                .GetString(HashService.Error.CalculateHashInvalidValue, _culture);

            // Assert
            Assert.Contains(response, c => c.Message == notificatioEvent);
        }
    }
}
