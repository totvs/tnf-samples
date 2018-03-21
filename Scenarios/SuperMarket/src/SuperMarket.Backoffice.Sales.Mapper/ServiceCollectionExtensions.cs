using Microsoft.Extensions.DependencyInjection;

namespace SuperMarket.Backoffice.Sales.Mapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesMapperDependency(this IServiceCollection services)
        {
            services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<InfraToDomainProfile>();
            });

            return services;
        }
    }
}
