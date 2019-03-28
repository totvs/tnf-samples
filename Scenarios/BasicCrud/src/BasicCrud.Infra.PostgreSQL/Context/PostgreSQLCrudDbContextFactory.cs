using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Tnf.Drivers.DevartPostgreSQL;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.PostgreSQL.Context
{
    public class PostgreSQLCrudDbContextFactory : IDesignTimeDbContextFactory<PostgreSQLCrudDbContext>
    {
        public PostgreSQLCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CrudDbContext>();

            var configuration = new ConfigurationBuilder()
                                     .SetBasePath(Directory.GetCurrentDirectory())
                                     .AddJsonFile($"appsettings.Development.json", false)
                                     .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            PosgreSqlLicense.Validade(databaseConfiguration.ConnectionString);

            builder.UsePostgreSql(databaseConfiguration.ConnectionString);

            return new PostgreSQLCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
