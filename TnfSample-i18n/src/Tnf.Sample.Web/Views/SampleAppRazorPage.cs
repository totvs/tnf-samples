using Abp.AspNetCore.Mvc.Views;
using Tnf.Localization;

namespace Tnf.Sample.Web.Views
{
    public abstract class SampleAppRazorPage<TModel> : TnfRazorPage<TModel>
    {
        protected SampleAppRazorPage()
        {
            LocalizationSourceName = SampleAppConsts.LocalizationSourceName;
        }
    }


    public abstract class TnfRazorPage<TModel> : AbpRazorPage<TModel>
    {
        new public ILocalizationManager LocalizationManager { get; set; }
    }
}
