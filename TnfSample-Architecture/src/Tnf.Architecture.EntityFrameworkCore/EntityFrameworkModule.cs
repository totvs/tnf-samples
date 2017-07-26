using Microsoft.EntityFrameworkCore;
using Tnf.App.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Dapper;
using Tnf.EntityFrameworkCore.Configuration;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.EntityFrameworkCore
{
    [DependsOn(
        typeof(TnfDapperModule),
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
            IocManager.RegisterAssemblyByConvention(typeof(EntityFrameworkModule).GetAssembly());
        }
    }
}