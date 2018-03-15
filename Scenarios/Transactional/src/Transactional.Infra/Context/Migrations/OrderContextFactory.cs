using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;
using Transactional.Domain;

namespace Transactional.Infra.Context.Migrations
{
    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OrderContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json", true)
                                    .Build();

            builder.UseSqlServer(configuration.GetConnectionString(Constants.ConnectionStringName));

            return new OrderContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
