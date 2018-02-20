using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace Case5.Infra.Context.Migration
{
    public class SettingDbContextFactory : IDesignTimeDbContextFactory<SettingDbContext>
    {
        public SettingDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SettingDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json", true)
                                    .Build();

            builder.UseOracle(configuration.GetConnectionString(InfraConsts.ConnectionStringName));

            return new SettingDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}