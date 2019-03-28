using Microsoft.Extensions.Configuration;
using System;

namespace ProdutoXyz.Infra
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(IConfiguration configuration)
        {
            ConnectionStringName = configuration["DefaultConnectionString"];

            if (DatabaseType.Sqlite.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.Sqlite;
            }
            else if (DatabaseType.Postgres.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.Postgres;
            }
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
        Sqlite,
        Postgres
    }
}