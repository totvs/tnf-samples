using System.Threading.Tasks;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Tnf.Architecture.Web.Views.Shared.Components.LanguageSelection
{
    public class LanguageSelectionViewComponent : ViewComponent
    {
        private readonly ILanguageManager _localizationManager;

        public LanguageSelectionViewComponent(ILanguageManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _localizationManager.CurrentLanguage,
                Languages = _localizationManager.GetLanguages(),
                CurrentUrl = Request.Path
            };

            return View(model);
        }
    }
}
