using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace Case2.Infra.Context.Migration
{
    public class LocalizationDbContextFactory : IDesignTimeDbContextFactory<LocalizationDbContext>
    {
        public LocalizationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LocalizationDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json", true)
                                    .Build();

            builder.UseSqlServer(configuration.GetConnectionString(InfraConsts.ConnectionStringName));

            return new LocalizationDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}