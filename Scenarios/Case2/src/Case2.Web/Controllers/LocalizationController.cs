using Case2.Infra;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using Tnf;
using Tnf.Localization;
using Tnf.Settings.Management;

namespace Case2.Web.Controllers
{
    [Route("api/localizations")]
    public class LocalizationController : TnfController
    {
        private readonly ISettingManager _settingManager;
        private readonly ILocalizationManager _localizationManager;

        public LocalizationController(
            ISettingManager settingManager,
            ILocalizationManager localizationManager)
        {
            _settingManager = settingManager;
            _localizationManager = localizationManager;
        }

        [HttpGet("culture")]
        public async Task<IActionResult> Get()
        {
            var culture = await _settingManager.GetSettingValueAsync(TnfConsts.Localization.DefaultLanguage);

            return Json(culture);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery]string culture)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(culture);

            await _settingManager.ChangeSettingForApplicationAsync(TnfConsts.Localization.DefaultLanguage, cultureInfo.Name);

            return Ok();
        }

        [HttpGet("culture/key")]
        public IActionResult GetKey()
        {
            var result = _localizationManager
                .GetSource(InfraConsts.LocalizationSourceName)
                .GetString(LocalizationKeys.CustomMessage);

            return Json(result);
        }
    }
}
