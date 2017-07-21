using Tnf.App.AspNetCore.Mvc.Controllers;
using Tnf.Architecture.Common;

namespace Tnf.Architecture.Web.Controllers
{
    public class ArchitectureControllerBase : TnfAppController
    {
        protected ArchitectureControllerBase()
        {
            LocalizationSourceName = AppConsts.LocalizationSourceName;
        }
    }
}
