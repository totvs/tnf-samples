using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using SuperMarket.Backoffice.Sales.Domain.Services;

namespace SuperMarket.Backoffice.Sales.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesDomainDependency(this IServiceCollection services)
        {
            services.AddTnfDomain();

            services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();
            
            return services;
        }
    }
}
