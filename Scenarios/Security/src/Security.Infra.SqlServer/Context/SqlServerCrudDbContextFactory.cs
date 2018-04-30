using Security.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace Security.Infra.SqlServer.Context
{
    public class SqlServerCrudDbContextFactory : IDesignTimeDbContextFactory<SqlServerCrudDbContext>
    {
        public SqlServerCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CrudDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", true)
                                    .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            return new SqlServerCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
