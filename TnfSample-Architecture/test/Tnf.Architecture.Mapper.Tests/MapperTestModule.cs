using Tnf.App.TestBase;
using Tnf.Modules;

namespace Tnf.Architecture.Mapper.Tests
{
    [DependsOn(
        typeof(MapperModule),
        typeof(TnfAppTestBaseModule))]
    public class MapperTestModule : TnfModule
    {
        public override void Initialize()
        {
            base.Initialize();

            IocManager.RegisterAssemblyByConvention(typeof(MapperTestModule).Assembly);
        }
    }
}
