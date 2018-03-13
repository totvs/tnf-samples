using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services)
        {
            return services
                .AddTnfEntityFrameworkCore()    // Configura o uso do EntityFrameworkCore registrando os contextos que serão usados pela aplicação
                .AddMapperDependency();         // Configura o uso do AutoMappper
        }
    }
}
