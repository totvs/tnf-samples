using Microsoft.AspNetCore.Mvc;
using Tnf.Localization;

namespace HelloWorld.Web.Controllers
{
    [Route("api/hello")]
    public class HelloController : TnfController
    {
        private readonly ILocalizationManager localizationManager;

        public HelloController(ILocalizationManager localizationManager)
        {
            this.localizationManager = localizationManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            var localizedHelloMessage = localizationSource.GetString(GlobalizationKey.Hello);

            return Json(localizedHelloMessage);
        }
    }
}
