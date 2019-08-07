using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProdutoXyz.Infra.Context;
using Tnf.Drivers.DevartPostgreSQL;
using Tnf.Runtime.Session;

namespace ProdutoXyz.Infra.Postgres.Context
{
    public class PostgresCrudDbContextFactory : IDesignTimeDbContextFactory<PostgresCrudDbContext>
    {
        public PostgresCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CrudDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            PostgreSqlLicense.Validate(databaseConfiguration.ConnectionString);

            builder.UsePostgreSql(databaseConfiguration.ConnectionString);

            return new PostgresCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
