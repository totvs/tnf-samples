namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddQueueDependency(this IServiceCollection services)
        {
            // Adiciona as principais dependencias do Tnf
            services.AddTnfKernel();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Configura o uso do client para fila
            services.AddTnfBusClient();
        }
    }
}
