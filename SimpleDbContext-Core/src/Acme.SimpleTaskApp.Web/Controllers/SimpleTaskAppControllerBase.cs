
using Tnf.AspNetCore.Mvc.Controllers;

namespace Acme.SimpleTaskApp.Web.Controllers
{
    public abstract class SimpleTaskAppControllerBase : TnfController
    {
        protected SimpleTaskAppControllerBase()
        {
            LocalizationSourceName = SimpleTaskAppConsts.LocalizationSourceName;
        }
    }
}