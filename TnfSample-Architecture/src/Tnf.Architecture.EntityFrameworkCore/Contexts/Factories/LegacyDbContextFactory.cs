using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Tnf.Architecture.Common;
using Tnf.Architecture.Domain.Configuration;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts.Factories
{
    public class LegacyDbContextFactory : IDesignTimeDbContextFactory<LegacyDbContext>
    {
        public LegacyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LegacyDbContext>();

            var assemblyPath = typeof(EntityFrameworkModule).GetAssembly().Location;
            var assemblyName = typeof(EntityFrameworkModule).GetAssembly().GetName().Name;

            var assemblyConfig = assemblyPath.Substring(0, assemblyPath.IndexOf(@"\src\", StringComparison.Ordinal) + 5);
            assemblyConfig = Path.Combine(assemblyConfig, assemblyName);

            var configuration = AppConfigurations.Get(assemblyConfig);

            DbContextConfigurer.Configure(builder, configuration.GetConnectionString(AppConsts.ConnectionStringName));

            return new LegacyDbContext(builder.Options);
        }
    }
}
