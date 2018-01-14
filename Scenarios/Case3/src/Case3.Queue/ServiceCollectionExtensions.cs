namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddQueueDependency(this IServiceCollection services)
        {
            // Adiciona as principais dependencias do Tnf
            services.AddTnfKernel();

            // Adiciona os registros automaticos de injeção de dependencias do Tnf
            // ITransientDependency
            // IScopedDependency
            // ISingletonDependecy
            services.AddTnfDefaultConventionalRegistrations();

            // Configura o uso do client para fila
            services.AddTnfBusClient();
        }
    }
}
