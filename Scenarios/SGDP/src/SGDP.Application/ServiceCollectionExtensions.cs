using Microsoft.Extensions.DependencyInjection;
using SGDP.Application.Services;
using SGDP.Application.Services.Interfaces;
using SGDP.Domain;

namespace SGDP.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
        {
            // Dependencia do projeto BasicCrud.Domain
            services.AddDomainDependency();

            // Registro dos serviços
            services.AddTransient<ICustomerAppService, CustomerAppService>();
            services.AddTransient<ICompanyAppService, CompanyAppService>();

            return services;
        }
    }
}
