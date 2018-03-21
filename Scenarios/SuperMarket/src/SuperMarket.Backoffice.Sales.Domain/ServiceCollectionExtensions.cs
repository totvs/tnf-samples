using Microsoft.Extensions.DependencyInjection;

namespace SuperMarket.Backoffice.Sales.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesDomainDependency(this IServiceCollection services)
        {
            services.AddTnfDomain();

            return services;
        }
    }
}
