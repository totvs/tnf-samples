using System.Reflection;
using Tnf.Modules;
using Tnf.Sample.Core;

namespace Tnf.Sample.Application
{
    [DependsOn(
        typeof(SampleCoreModule))]
    public class SampleAppModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
