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
                                     .AddJsonFile($"appsettings.Development.json", false)
                                     .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            new ServiceCollection()
                .AddTnfKernel()
                .BuildServiceProvider()
                .ConfigureTnf(tnf =>
                {
                    tnf.DefaultNameOrConnectionString = databaseConfiguration.ConnectionString;
                    tnf.EnableDevartOracleDriver();
                });

            DevartOracleSettings.SetDefaultSettings();

            builder.UseOracle(databaseConfiguration.ConnectionString);

            return new OracleCrudDbContext(builder.Options, NullTnfSession.Instance, databaseConfiguration);
        }
    }
}
