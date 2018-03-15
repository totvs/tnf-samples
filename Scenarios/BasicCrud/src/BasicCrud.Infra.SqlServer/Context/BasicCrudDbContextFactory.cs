using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.SqlServer.Context
{
    public class BasicCrudDbContextFactory : IDesignTimeDbContextFactory<BasicCrudDbContext>
    {
        public BasicCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BasicCrudDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json", true)
                                    .Build();
            
            builder.UseSqlServer(configuration.GetConnectionString(SqlServerConstants.ConnectionStringName));

            return new BasicCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
