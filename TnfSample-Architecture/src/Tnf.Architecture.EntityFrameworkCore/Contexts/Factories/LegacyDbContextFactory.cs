using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Tnf.Architecture.Common;
using Tnf.Architecture.Domain.Configuration;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts.Factories
{
    public class LegacyDbContextFactory : IDbContextFactory<LegacyDbContext>
    {
        public LegacyDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<LegacyDbContext>();
            var configuration = AppConfigurations.Get(typeof(EntityFrameworkModule).GetAssembly().Location);

            DbContextConfigurer.Configure(builder, configuration.GetConnectionString(AppConsts.ConnectionStringName));

            return new LegacyDbContext(builder.Options);
        }
    }
}
