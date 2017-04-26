using Tnf.Architecture.CrossCutting;
using Tnf.AspNetCore.Mvc.Controllers;

namespace Tnf.Architecture.Web.Controllers
{
    public class ArchitectureControllerBase : TnfController
    {
        protected ArchitectureControllerBase()
        {
            LocalizationSourceName = AppConsts.LocalizationSourceName;
        }
    }
}
