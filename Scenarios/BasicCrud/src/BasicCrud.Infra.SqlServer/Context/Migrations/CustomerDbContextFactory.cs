using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.SqlServer.Context.Migrations
{
    public class CustomerDbContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
    {
        public CustomerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CustomerDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json", true)
                                    .Build();
            
            builder.UseSqlServer(configuration.GetConnectionString(SqlServerConstants.ConnectionStringName));

            return new CustomerDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
