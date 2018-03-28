using Querying.Infra.Context;
using Querying.Infra.Repositories;

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
                .AddTnfDbContext<PurchaseOrderContext>(config => DbContextConfigurer.Configure(config));

            services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
        }
    }
}
