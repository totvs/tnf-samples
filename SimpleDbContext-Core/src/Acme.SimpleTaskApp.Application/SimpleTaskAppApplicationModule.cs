using Tnf.AutoMapper;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Acme.SimpleTaskApp
{
    [DependsOn(
        typeof(SimpleTaskAppCoreModule), 
        typeof(TnfAutoMapperModule))]
    public class SimpleTaskAppApplicationModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppApplicationModule).GetAssembly());
        }
    }
}