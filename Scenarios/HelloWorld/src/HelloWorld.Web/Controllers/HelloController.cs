using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
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
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult Get()
        {
            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            var localizedHelloMessage = localizationSource.GetString(GlobalizationKey.Hello);

            return Json(localizedHelloMessage);
        }
    }
}
