using Tnf.App.EntityFrameworkCore;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Sample.EntityFrameworkCore
{
    [DependsOn(
        typeof(SampleCoreModule), 
        typeof(TnfAppEntityFrameworkCoreModule))]
    public class SampleEntityFrameworkCoreModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SampleEntityFrameworkCoreModule).GetAssembly());
        }
    }
}