using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Tnf.Runtime.Session;

namespace TnfZero.EntityFramework.PostgreSQL;

/// <summary>
///     Design-time factory used by EF Core tooling (dotnet ef migrations add / update).
///     Reads the connection string from appsettings.Development.json in the project directory.
/// </summary>
public class PostgreSqlTnfZeroDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlTnfZeroDbContext>
{
    public PostgreSqlTnfZeroDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TnfZeroDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));

        return new PostgreSqlTnfZeroDbContext(optionsBuilder.Options, NullTnfSession.Instance);
    }
}