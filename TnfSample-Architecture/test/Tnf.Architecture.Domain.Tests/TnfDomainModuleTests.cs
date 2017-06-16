using Tnf.App;
using Tnf.App.TestBase;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Domain.Tests
{
    [DependsOn(
        typeof(TnfAppModule),
        typeof(TnfAppTestBaseModule))]
    public class TnfDomainModuleTests : TnfModule
    {
        public override void Initialize()
        {
            base.Initialize();

            IocManager.RegisterAssemblyByConvention(typeof(TnfDomainModuleTests).GetAssembly());
        }
    }
}
