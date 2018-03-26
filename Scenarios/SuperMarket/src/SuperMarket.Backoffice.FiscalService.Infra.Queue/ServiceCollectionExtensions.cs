using Microsoft.Extensions.DependencyInjection;

namespace SuperMarket.Backoffice.FiscalService.Infra.Queue
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFiscalInfraQueueDependency(this IServiceCollection services)
        {
            // Configura o uso do client para fila
            services.AddTnfBusClient();

            return services;
        }
    }
}
