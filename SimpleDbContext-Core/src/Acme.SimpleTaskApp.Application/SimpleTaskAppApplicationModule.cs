using System.Reflection;
using Tnf.AutoMapper;
using Tnf.Modules;

namespace Acme.SimpleTaskApp
{
    [DependsOn(
        typeof(SimpleTaskAppCoreModule), 
        typeof(TnfAutoMapperModule))]
    public class SimpleTaskAppApplicationModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}