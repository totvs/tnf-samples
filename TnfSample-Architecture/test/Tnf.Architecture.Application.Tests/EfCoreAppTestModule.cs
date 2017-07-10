using Tnf.App.EntityFrameworkCore;
using Tnf.App.TestBase;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Application.Tests
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfAppTestBaseModule))]
    public class EfCoreAppTestModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration
                .TnfEfCoreInMemory()
                .RegisterDbContextInMemory<ArchitectureDbContext>()
                .RegisterDbContextInMemory<LegacyDbContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EfCoreAppTestModule).GetAssembly());
        }
    }
}