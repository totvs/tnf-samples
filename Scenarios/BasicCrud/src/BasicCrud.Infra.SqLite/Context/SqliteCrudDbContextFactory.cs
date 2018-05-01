using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.SqLite.Context
{
    public class SqliteCrudDbContextFactory : IDesignTimeDbContextFactory<SqliteCrudDbContext>
    {
        public SqliteCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CrudDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            builder.UseSqlite(databaseConfiguration.ConnectionString);

            return new SqliteCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
