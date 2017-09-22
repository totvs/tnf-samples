using System;
using Tnf.App.AspNetCore.TestBase;
using Tnf.App.EntityFrameworkCore;
using Tnf.Architecture.Application;
using Tnf.Architecture.Carol.ReadInterfaces;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Architecture.Web.Tests.Mocks;
using Tnf.Configuration.Startup;
using Tnf.Modules;

namespace Tnf.Architecture.Web.Tests.App
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfAppAspNetCoreTestBaseModule))]
    public class AppTestModule : TnfModule
    {
        public override void PreInitialize()
        {
            // Mock repositories
            Configuration.ReplaceService<IWhiteHouseRepository, WhiteHouseRepositoryMock>();
            Configuration.ReplaceService<IWhiteHouseReadRepository, WhiteHouseReadRepositoryMock>();

            Configuration
                .TnfEfCoreInMemory(IocManager.Resolve<IServiceProvider>())
                .RegisterDbContextInMemory<ArchitectureDbContext>()
                .RegisterDbContextInMemory<LegacyDbContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppTestModule).Assembly);
        }
    }
}
