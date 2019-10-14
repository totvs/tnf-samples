using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SuperMarket.FiscalService.Infra
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;

            ConnectionStringName = configuration["DefaultConnectionString"];

            var connectionString = configuration[$"ConnectionStrings:{ConnectionStringName}"];

            if (DatabaseType.SqlServer.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.SqlServer;
            }
            else
                throw new NotSupportedException($"Invalid ConnectionString name '{ConnectionStringName}'.");

            ConnectionString = connectionString;
        }

        public IConfiguration Configuration { get; }
        public string ConnectionStringName { get; }
        public string ConnectionString { get; set; }
        public DatabaseType DatabaseType { get; }

        public static DatabaseConfiguration LoadForMigrations()
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.Migrator.json", false)
                    .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            return databaseConfiguration;
        }
    }

    public enum DatabaseType
    {
        SqlServer
    }
}
