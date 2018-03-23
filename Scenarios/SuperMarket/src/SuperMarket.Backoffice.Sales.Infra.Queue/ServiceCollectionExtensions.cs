using Microsoft.Extensions.DependencyInjection;

namespace SuperMarket.Backoffice.Sales.Infra.Queue
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesInfraQueueDependency(this IServiceCollection services)
        {
            // Configura o uso do client para fila
            services.AddTnfBusClient();

            return services;
        }
    }
}
