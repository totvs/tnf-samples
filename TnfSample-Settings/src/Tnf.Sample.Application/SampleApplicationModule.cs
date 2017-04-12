using System.Reflection;
using Tnf.AutoMapper;
using Tnf.Modules;

namespace Tnf.Sample
{
    [DependsOn(
        typeof(SampleCoreModule), 
        typeof(TnfAutoMapperModule))]
    public class SampleApplicationModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
        }
    }
}