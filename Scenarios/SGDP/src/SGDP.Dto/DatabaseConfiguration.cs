using Microsoft.Extensions.Configuration;
using System;

namespace SGDP.Infra
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(IConfiguration configuration)
        {
            DefaultSchema = configuration["DefaultSchema"];
            ConnectionStringName = configuration["DefaultConnectionString"];

            if (DatabaseType.PostgreSQL.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.PostgreSQL;
            }
            else
                throw new NotSupportedException($"Invalid ConnectionString name '{ConnectionStringName}'.");

            ConnectionString = configuration[$"ConnectionStrings:{ConnectionStringName}"];
        }

        public string DefaultSchema { get; }
        public string ConnectionStringName { get; }
        public string ConnectionString { get; }
        public DatabaseType DatabaseType { get; }
    }

    public enum DatabaseType
    {
        PostgreSQL
    }
}
