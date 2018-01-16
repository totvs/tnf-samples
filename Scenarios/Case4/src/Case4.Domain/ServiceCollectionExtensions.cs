namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomainDependency(this IServiceCollection services)
        {
            // Adiciona as principais dependencias do Tnf
            services.AddTnfKernel();

            // Adiciona os registros automaticos de injeção de dependencias do Tnf
            // ITransientDependency
            // IScopedDependency
            // ISingletonDependecy
            services.AddTnfDefaultConventionalRegistrations();

            // Adiciona as dependencias para utilização dos serviços de crud generico
            services.AddTnfDomain();
        }
    }
}
