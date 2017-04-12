using Tnf.AspNetCore.Mvc.Views;

namespace Tnf.Sample.Web.Views
{
    public abstract class SampleAppRazorPage<TModel> : TnfRazorPage<TModel>
    {
        protected SampleAppRazorPage()
        {
        }
    }
}
