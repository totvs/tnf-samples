using ProdutoXyz.Application.Services;
using ProdutoXyz.Application.Services.Interfaces;
using ProdutoXyz.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace ProdutoXyz.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
        {
            // Dependencia do projeto Security.Domain
            services.AddDomainDependency();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Registro dos serviços
            services.AddTransient<IProductAppService, ProductAppService>();

            return services;
        }
    }
}
