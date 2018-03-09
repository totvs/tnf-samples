using HelloWorld.SharedKernel.Conventionals;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KernelServiceCollectionExtensions
    {
        public static void AddSharedKernelDependency(this IServiceCollection services)
        {
            // Adiciona as dependencias do Tnf
            services
                .AddTnfKernel()
                .AddTnfNotifications();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Exemplo de um registro customizado de interface para DI por convenção
            services.AddConventionalRegistrationOfType<IRegisterTransientDependency>(ServiceLifetime.Transient);
        }
    }
}
