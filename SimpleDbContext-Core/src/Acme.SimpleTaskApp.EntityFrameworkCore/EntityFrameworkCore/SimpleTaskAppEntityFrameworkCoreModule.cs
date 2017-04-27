using Tnf.EntityFrameworkCore;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Acme.SimpleTaskApp.EntityFrameworkCore
{
    [DependsOn(
        typeof(SimpleTaskAppCoreModule), 
        typeof(TnfEntityFrameworkCoreModule))]
    public class SimpleTaskAppEntityFrameworkCoreModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppEntityFrameworkCoreModule).GetAssembly());
        }
    }
}