using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Tnf.Runtime.Session;

namespace Tnf.CarShop.EntityFramework.PostgreSql
{
    public class PostgreSqlCarShopDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlCarShopDbContext>
    {
        public PostgreSqlCarShopDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.Development.json", optional: false)
            .Build();

            var builder = new DbContextOptionsBuilder<CarShopDbContext>();

            builder.UseNpgsql(configuration.GetConnectionString("PostgreSql"));

            return new PostgreSqlCarShopDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
