using System.Reflection;
using Tnf.EntityFrameworkCore;
using Tnf.Modules;

namespace Acme.SimpleTaskApp.EntityFrameworkCore
{
    [DependsOn(
        typeof(SimpleTaskAppCoreModule), 
        typeof(TnfEntityFrameworkCoreModule))]
    public class SimpleTaskAppEntityFrameworkCoreModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}