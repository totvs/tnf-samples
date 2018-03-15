using Messaging.Infra1.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfra1Dependency(this IServiceCollection services)
        {
            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Configura o uso do cache em memoria
            services.AddTransient<INotifierService, NotifierService>();

            return services;
        }
    }
}
