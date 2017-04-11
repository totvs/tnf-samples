using Tnf.Architecture.CrossCutting;
using Tnf.AspNetCore.Mvc.Views;
using Tnf.Localization;

namespace Tnf.Architecture.Web.Views
{
    public abstract class AppRazorPage<TModel> : TnfRazorPage<TModel>
    {
        protected AppRazorPage()
        {
            LocalizationSourceName = AppConsts.LocalizationSourceName;
        }

        new public ILocalizationManager LocalizationManager { get; set; }
    }
}
