using ProdutoXyz.Infra.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace ProdutoXyz.Infra
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapperDependency(this IServiceCollection services)
        {
            // Configura o uso do AutoMappper
            return services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<BasicCrudProfile>();
            });
        }
    }
}
