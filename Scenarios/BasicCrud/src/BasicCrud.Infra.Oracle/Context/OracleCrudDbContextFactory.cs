using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.Oracle.Context
{
    public class OracleCrudDbContextFactory : IDesignTimeDbContextFactory<OracleCrudDbContext>
    {
        public OracleCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CrudDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json", true)
                                    .AddJsonFile($"appsettings.Development.json", true)
                                    .Build();

            var provider = new ServiceCollection()
                .AddTnfKernel()
                .BuildServiceProvider();

            var tnfConfiguration = provider.ConfigureTnf();

            tnfConfiguration.DefaultNameOrConnectionString = configuration.GetConnectionString(DatabaseType.Oracle.ToString());
            tnfConfiguration.EnableDevartOracleDriver();

            builder.UseOracle(tnfConfiguration.DefaultNameOrConnectionString);

            return new OracleCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
