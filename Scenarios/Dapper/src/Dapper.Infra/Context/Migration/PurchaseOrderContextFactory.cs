using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace Dapper.Infra.Context.Migration
{
    public class PurchaseOrderContextFactory : IDesignTimeDbContextFactory<PurchaseOrderContext>
    {
        public PurchaseOrderContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PurchaseOrderContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            builder.UseSqlServer(configuration.GetConnectionString(Constants.ConnectionStringName));

            return new PurchaseOrderContext(builder.Options, NullTnfSession.Instance);
        }
    }
}