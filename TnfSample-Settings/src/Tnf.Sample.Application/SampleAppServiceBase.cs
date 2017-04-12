
using Tnf.Application.Services;

namespace Tnf.Sample
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class SampleAppServiceBase : ApplicationService
    {
        protected SampleAppServiceBase()
        {
            LocalizationSourceName = SampleAppConsts.LocalizationSourceName;
        }
    }
}