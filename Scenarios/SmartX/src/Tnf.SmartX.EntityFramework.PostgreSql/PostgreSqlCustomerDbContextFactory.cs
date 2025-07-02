using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Tnf.Runtime.Session;

namespace Tnf.SmartX.EntityFramework.PostgreSql;

public class PostgreSqlCustomerDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlCustomerDbContext>
{
    public PostgreSqlCustomerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", false)
            .Build();

        var builder = new DbContextOptionsBuilder<CustomerDbContext>();

        builder.UseNpgsql(configuration.GetConnectionString("CodeFirstCustomer"));

        return new PostgreSqlCustomerDbContext(builder.Options, NullTnfSession.Instance);
    }
}
