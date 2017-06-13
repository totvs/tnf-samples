using Tnf.App.Dto.Response;
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
