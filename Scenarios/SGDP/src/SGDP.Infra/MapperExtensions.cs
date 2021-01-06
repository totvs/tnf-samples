using Microsoft.Extensions.DependencyInjection;
using SGDP.Infra.MapperProfiles;

namespace SGDP.Infra
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapperDependency(this IServiceCollection services)
        {
            // Configura o uso do AutoMappper
            return services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<SgdpProfile>();
            });
        }
    }
}
