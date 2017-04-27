using Tnf.AutoMapper;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Sample
{
    [DependsOn(
        typeof(SampleCoreModule), 
        typeof(TnfAutoMapperModule))]
    public class SampleApplicationModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SampleApplicationModule).GetAssembly());
            
        }
    }
}