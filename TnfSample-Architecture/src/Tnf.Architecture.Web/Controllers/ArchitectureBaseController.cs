using Tnf.App.AspNetCore.Mvc.Controllers;
using Tnf.Architecture.Dto;
using Tnf.Runtime.Validation;

namespace Tnf.Architecture.Web.Controllers
{
    [DisableValidation]
    public class ArchitectureControllerBase : TnfAppController
    {
        protected ArchitectureControllerBase()
        {
            LocalizationSourceName = AppConsts.LocalizationSourceName;
        }
    }
}
