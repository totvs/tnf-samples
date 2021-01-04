using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Tnf.Drivers.DevartPostgreSQL;
using Tnf.Runtime.Session;

namespace SGDP.Infra.Context
{
    public class PostgreSQLCrudDbContextFactory : IDesignTimeDbContextFactory<PostgreSQLCrudDbContext>
    {
        public PostgreSQLCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OrderDbContext>();

            var configuration = new ConfigurationBuilder()
                                     .SetBasePath(Directory.GetCurrentDirectory())
                                     .AddJsonFile($"appsettings.Development.json", false)
                                     .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            PostgreSqlLicense.Validate(databaseConfiguration.ConnectionString);

            builder.UsePostgreSql(databaseConfiguration.ConnectionString);

            return new PostgreSQLCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
