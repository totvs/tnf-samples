using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Infra.SqLite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqLiteDependency(this IServiceCollection services)
        {
            services
                .AddInfraDependency()
                .AddTnfDbContext<CustomerDbContext>((config) =>
                {
                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlite(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlite(config.ConnectionString);
                });

            return services;
        }
    }
}
