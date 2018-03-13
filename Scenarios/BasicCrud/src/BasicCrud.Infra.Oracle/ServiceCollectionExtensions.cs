using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Infra.Oracle
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOracleDependency(this IServiceCollection services)
        {
            services
                .AddInfraDependency()
                .AddTnfDbContext<CustomerDbContext>((config) =>
                {
                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseOracle(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseOracle(config.ConnectionString);
                });

            return services;
        }
    }
}
