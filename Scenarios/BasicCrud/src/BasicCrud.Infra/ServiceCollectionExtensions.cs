using BasicCrud.Infra.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services)
        {
            services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<CustomerProfile>();
            });

            return services;
        }
    }
}
