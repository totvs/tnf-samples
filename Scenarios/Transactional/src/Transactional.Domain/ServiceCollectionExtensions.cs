using Microsoft.Extensions.DependencyInjection;
using Transactional.Domain.Interfaces;
using Transactional.Domain.Services;

namespace Transacional.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainDependency(this IServiceCollection services)
        {
            // Adiciona as dependências do Tnf de notificação
            services.AddTnfNotifications();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Registro dos serviços
            services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();

            return services;
        }
    }
}
