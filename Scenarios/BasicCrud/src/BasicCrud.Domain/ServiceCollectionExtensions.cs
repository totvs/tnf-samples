using BasicCrud.Domain.Interfaces.Services;
using BasicCrud.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainDependency(this IServiceCollection services)
        {
            // Adiciona as dependencias para utilização dos serviços de crud generico do Tnf
            services.AddTnfDomain();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Registro dos serviços
            services.AddTransient<IProductDomainService, ProductDomainService>();

            return services;
        }
    }
}
