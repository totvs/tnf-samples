
using Tnf.AspNetCore.Mvc.Controllers;

namespace Tnf.Sample.Web.Controllers
{
    public abstract class SimpleTaskAppControllerBase : TnfController
    {
        protected SimpleTaskAppControllerBase()
        {
            LocalizationSourceName = SampleAppConsts.LocalizationSourceName;
        }
    }
}