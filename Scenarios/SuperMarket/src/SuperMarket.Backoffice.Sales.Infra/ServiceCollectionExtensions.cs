using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Sales.Domain;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using SuperMarket.Backoffice.Sales.Infra.Repositories;
using SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces;

namespace SuperMarket.Backoffice.Sales.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesInfraDependency(this IServiceCollection services)
        {
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<SalesContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.DbContextOptions.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.QueryClientEvaluationWarning));
                        config.UseLoggerFactory();
                    }

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlServer(config.ConnectionString);
                });

            services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddTransient<IPurchaseOrderReadRepository, PurchaseOrderReadRepository>();
            services.AddTransient<IPriceTableRepository, PriceTableRepository>();

            return services;
        }
    }
}
