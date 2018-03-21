using Microsoft.Extensions.DependencyInjection;

namespace SuperMarket.Backoffice.Crud.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCrudDomainDependency(this IServiceCollection services)
        {
            services.AddTnfDomain();

            return services;
        }
    }
}
