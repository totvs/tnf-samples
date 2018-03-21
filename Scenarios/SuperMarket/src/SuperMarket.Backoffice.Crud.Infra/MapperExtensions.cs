using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Infra.AutoMapperProfiles;

namespace SuperMarket.Backoffice.Crud.Infra
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapperDependency(this IServiceCollection services)
        {
            // Configura o uso do AutoMappper
            return services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<InfraToDtoProfile>();
            });
        }
    }
}
