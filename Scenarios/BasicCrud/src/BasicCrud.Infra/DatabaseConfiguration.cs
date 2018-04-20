using Microsoft.Extensions.Configuration;
using System;

namespace BasicCrud.Infra
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(IConfiguration configuration)
        {
            ConnectionStringName = configuration["DefaultConnectionString"];

            if (DatabaseType.SqlServer.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
                DatabaseType = DatabaseType.SqlServer;
            else if (DatabaseType.Sqlite.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
                DatabaseType = DatabaseType.Sqlite;
            else if (DatabaseType.Oracle.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
                DatabaseType = DatabaseType.Oracle;
            else
                throw new NotSupportedException($"Invalid ConnectionString name '{ConnectionStringName}'.");

            ConnectionString = configuration[$"ConnectionStrings:{ConnectionStringName}"];
        }

        public string ConnectionStringName { get; }
        public string ConnectionString { get; }
        public DatabaseType DatabaseType { get; }
    }

    public enum DatabaseType
    {
        SqlServer,
        Sqlite,
        Oracle
    }
}
