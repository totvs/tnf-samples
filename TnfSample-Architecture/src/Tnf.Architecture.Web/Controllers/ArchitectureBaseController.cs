using Tnf.Architecture.Dto;
using Tnf.AspNetCore.Mvc.Controllers;
using Tnf.Runtime.Validation;

namespace Tnf.Architecture.Web.Controllers
{
    [DisableValidation]
    public class ArchitectureControllerBase : TnfController
    {
        protected ArchitectureControllerBase()
        {
            LocalizationSourceName = AppConsts.LocalizationSourceName;
        }
    }
}
