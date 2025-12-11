using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Tnf.Runtime.Session;

namespace Tnf.SmartX.EntityFramework.PostgreSql;

public class PostgreSqlCompanyDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlCompanyDbContext>
{
    public PostgreSqlCompanyDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", false)
            .Build();

        var builder = new DbContextOptionsBuilder<CompanyDbContext>();

        builder.UseNpgsql(configuration.GetConnectionString("CodeFirst"));

        return new PostgreSqlCompanyDbContext(builder.Options, NullTnfSession.Instance);
    }
}
