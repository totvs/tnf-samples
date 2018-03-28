using Messaging.Infra2;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfra2Dependency(this IServiceCollection services)
        {
            // Configura o uso do cache em memoria
            services.AddTnfMemoryCache();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Configura o uso do client para fila
            services.AddTnfBusClient();

            services.AddTransient<IMessageStoreService, MessageStoreService>();
        }
    }
}
