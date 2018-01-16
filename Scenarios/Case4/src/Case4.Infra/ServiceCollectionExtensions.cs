using Case4.Infra.Context;
using Case4.Infra.Mapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfraDependency(this IServiceCollection services)
        {
            // Configura o uso do EntityFrameworkCore registrando os contextos que serão
            // usados pela aplicação
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<CustomerDbContext>(config => DbContextConfigurer.Configure(config));

            // Configura o uso do AutoMappper
            services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<CustomerProfile>();
            });
        }
    }
}
