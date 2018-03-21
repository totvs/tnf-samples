using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SuperMarket.Backoffice.Sales.Domain;
using System.IO;
using Tnf.Runtime.Session;

namespace SuperMarket.Backoffice.Sales.Infra.Contexts
{
    public class SalesContextFactory : IDesignTimeDbContextFactory<SalesContext>
    {
        public SalesContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SalesContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            builder.UseSqlServer(configuration.GetConnectionString(Constants.ConnectionStringName));

            return new SalesContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
