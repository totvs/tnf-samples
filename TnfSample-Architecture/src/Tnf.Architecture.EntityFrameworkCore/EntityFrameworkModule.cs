using Microsoft.EntityFrameworkCore;
using Tnf.App.Dapper;
using Tnf.App.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.EntityFrameworkCore.Configuration;
using Tnf.Modules;

namespace Tnf.Architecture.EntityFrameworkCore
{
    [DependsOn(
        typeof(TnfAppDapperModule),
        typeof(TnfAppEntityFrameworkCoreModule))]
    public class EntityFrameworkModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.TnfEfCore().AddDbContext<LegacyDbContext>(configuration =>
            {
                configuration.DbContextOptions.UseSqlServer(configuration.ConnectionString);
            });

            Configuration.Modules.TnfEfCore().AddDbContext<ArchitectureDbContext>(configuration =>
            {
                configuration.DbContextOptions.UseSqlServer(configuration.ConnectionString);
            });
        }

        public override void Initialize()
        {
            base.Initialize();

            // Register all the interfaces and its implmentations on this assembly
            IocManager.RegisterAssemblyByConvention<EntityFrameworkModule>();
        }
    }
}