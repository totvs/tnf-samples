using Tnf.Architecture.Dto;
using Tnf.AspNetCore.Mvc.Controllers;
using Tnf.Runtime.Validation;
using Tnf.Web.Models;

namespace Tnf.Architecture.Web.Controllers
{
    [DisableValidation]
    [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
    public class ArchitectureControllerBase : TnfController
    {
        protected ArchitectureControllerBase()
        {
            LocalizationSourceName = AppConsts.LocalizationSourceName;
        }
    }
}
