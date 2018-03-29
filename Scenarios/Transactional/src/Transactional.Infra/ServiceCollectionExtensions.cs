using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transactional.Domain;
using Transactional.Domain.Interfaces;
using Transactional.Infra.Context;
using Transactional.Infra.Repositories;

namespace Transactional.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services)
        {
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<PurchaseOrderContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                        config.DbContextOptions.EnableSensitiveDataLogging();

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlServer(config.ConnectionString);
                });

            services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();

            return services;
        }
    }
}
