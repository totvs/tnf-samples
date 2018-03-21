using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SuperMarket.Backoffice.FiscalService.Domain;
using System.IO;
using Tnf.Runtime.Session;

namespace SuperMarket.Backoffice.FiscalService.Infra.Contexts
{
    public class FiscalContextFactory : IDesignTimeDbContextFactory<FiscalContext>
    {
        public FiscalContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FiscalContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            builder.UseSqlServer(configuration.GetConnectionString(Constants.ConnectionStringName));

            return new FiscalContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
