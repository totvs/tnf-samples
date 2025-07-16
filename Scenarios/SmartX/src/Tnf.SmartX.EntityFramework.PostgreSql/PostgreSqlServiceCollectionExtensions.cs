using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tnf.SmartX.EntityFramework.PostgreSql;

public static class PostgreSqlServiceCollectionExtensions
{
    private const string CodeFirstCustomerConnectionString = "CodeFirstCustomer";

    public static IServiceCollection AddEFCorePostgreSql(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddTnfDbContext<CompanyDbContext, PostgreSqlCompanyDbContext>(c =>
        {
            c.DbContextOptions.UseNpgsql(c.ConnectionString);
        });        

        services.AddTnfDbContext<CustomerDbContext, PostgreSqlCustomerDbContext>(c =>
        {
            c.DbContextOptions.UseNpgsql(configuration.GetConnectionString(CodeFirstCustomerConnectionString));
        });

        return services;
    }
}
