using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainDependency(this IServiceCollection services)
        {
            services
                // Adiciona as dependencias para utilização dos serviços de crud generico do Tnf
                .AddTnfDomain();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Exemplo de um registro customizado de interface para DI por convenção
            //services.AddConventionalRegistrationOfType<IRegisterTransientDependency>(ServiceLifetime.Transient);

            return services;
        }
    }
}
