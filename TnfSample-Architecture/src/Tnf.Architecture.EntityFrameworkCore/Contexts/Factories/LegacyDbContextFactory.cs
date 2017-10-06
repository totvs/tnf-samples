using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Tnf.Architecture.Common;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts.Factories
{
    public class LegacyDbContextFactory : IDesignTimeDbContextFactory<LegacyDbContext>
    {
        public LegacyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LegacyDbContext>();

            var assemblyPath = typeof(EntityFrameworkModule).Assembly.Location;
            var assemblyName = typeof(EntityFrameworkModule).Assembly.GetName().Name;

            var assemblyConfig = assemblyPath.Substring(0, assemblyPath.IndexOf(@"\src\", StringComparison.Ordinal) + 5);
            assemblyConfig = Path.Combine(assemblyConfig, assemblyName);

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(assemblyConfig)
                                    .AddJsonFile($"appsettings.json", true)
                                    .Build();

            DbContextConfigurer.Configure(builder, configuration.GetConnectionString(AppConsts.ConnectionStringName));

            return new LegacyDbContext(builder.Options);
        }
    }
}
