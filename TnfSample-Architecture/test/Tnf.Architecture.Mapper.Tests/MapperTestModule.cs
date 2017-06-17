using Tnf.Modules;
using Tnf.App.TestBase;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Mapper.Tests
{
    [DependsOn(typeof(MapperModule),
               typeof(TnfAppTestBaseModule))]
    public class MapperTestModule: TnfModule
    {
        public override void Initialize()
        {
            base.Initialize();

            IocManager.RegisterAssemblyByConvention(typeof(MapperTestModule).GetAssembly());
        }
    }
}
