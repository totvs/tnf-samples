using Tnf.AspNetCore;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Sample.Web.Startup
{
    [DependsOn(
        typeof(TnfAspNetCoreModule))]        
    public class SampleWebModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SampleWebModule).GetAssembly());
        }
    }
}