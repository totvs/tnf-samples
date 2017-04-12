using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Acme.SimpleTaskApp.Web.Views.Shared.Components.LanguageSelection
{
    public class LanguageSelectionViewComponent: ViewComponent
    {
        private readonly ILanguageManager _localizationManager;

        public LanguageSelectionViewComponent(ILanguageManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        public IViewComponentResult InvokeAsync()
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
