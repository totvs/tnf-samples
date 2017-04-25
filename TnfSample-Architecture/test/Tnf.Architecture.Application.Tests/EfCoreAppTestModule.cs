using Tnf.Modules;
using Tnf.TestBase;
using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Application.Tests
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfTestBaseModule))]
    public class EfCoreAppTestModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules
                .TnfEfCoreInMemory(IocManager.IocContainer)
                .RegisterDbContextInMemory<ArchitectureDbContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EfCoreAppTestModule).GetAssembly());
        }
    }
}