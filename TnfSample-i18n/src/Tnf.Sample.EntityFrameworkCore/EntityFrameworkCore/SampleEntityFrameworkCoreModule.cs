using System.Reflection;
using Tnf.App.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Modules;

namespace Tnf.Sample.EntityFrameworkCore
{
    [DependsOn(
        typeof(SampleCoreModule), 
        typeof(TnfAppEntityFrameworkCoreModule))]
    public class SampleEntityFrameworkCoreModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}