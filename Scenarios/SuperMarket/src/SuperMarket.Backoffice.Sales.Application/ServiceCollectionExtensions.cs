using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Sales.Domain;
using SuperMarket.Backoffice.Sales.Infra;

namespace SuperMarket.Backoffice.Sales.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesApplicationDependency(this IServiceCollection services)
        {
            services
                .AddSalesDomainDependency()
                .AddSalesInfraDependency();

            return services;
        }
    }
}
