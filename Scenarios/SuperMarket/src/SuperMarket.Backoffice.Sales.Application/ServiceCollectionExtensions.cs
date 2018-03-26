using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Sales.Application.Services;
using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Domain;
using SuperMarket.Backoffice.Sales.Infra;
using SuperMarket.Backoffice.Sales.Infra.Queue;
using SuperMarket.Backoffice.Sales.Mapper;

namespace SuperMarket.Backoffice.Sales.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesApplicationDependency(this IServiceCollection services)
        {
            services
                .AddSalesInfraDependency()
                .AddSalesInfraQueueDependency()
                .AddSalesDomainDependency()
                .AddSalesMapperDependency();

            services.AddTransient<IPurchaseOrderAppService, PurchaseOrderAppService>();

            return services;
        }
    }
}
