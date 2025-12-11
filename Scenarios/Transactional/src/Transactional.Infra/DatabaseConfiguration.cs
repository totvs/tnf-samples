using System;
using Microsoft.Extensions.Configuration;

namespace Transactional.Infra
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(IConfiguration configuration, bool validateLicenseKey = true)
        {
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

        public string ConnectionStringName { get; }
        public string ConnectionString { get; set; }
        public DatabaseType DatabaseType { get; }
    }

    public enum DatabaseType
    {
        SqlServer
    }
}
