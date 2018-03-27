using Microsoft.Extensions.DependencyInjection;

namespace SuperMarket.FiscalService.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFiscalDomainDependency(this IServiceCollection services)
        {
            services.AddTnfDomain();

            return services;
        }
    }
}
