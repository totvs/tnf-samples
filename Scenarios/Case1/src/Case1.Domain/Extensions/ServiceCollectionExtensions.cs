using Case1.Domain;

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

            // Exemplo de um registro customizado de interface para convenção
            services.AddConventionalRegistrationOfType<IRegisterTransientDependency>(ServiceLifetime.Transient);
        }
    }
}
