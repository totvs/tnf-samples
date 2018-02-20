using Case5.Infra.Context;
using Case5.Infra.Dapper;
using Case5.Infra.Mapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services)
        {
            // Adiciona as principais dependencias do Tnf
            services.AddTnfKernel();

            // Adiciona os registros automaticos de injeção de dependencias do Tnf
            // ITransientDependency
            // IScopedDependency
            // ISingletonDependecy
            services.AddTnfDefaultConventionalRegistrations();

            // Configura o uso do EntityFrameworkCore registrando os contextos que serão
            // usados pela aplicação
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<CustomerDbContext>(config => DbContextConfigurer.Configure(config))
                .AddTnfDbContext<EmployeeDbContext>(config => DbContextConfigurer.Configure(config))
                .AddTnfDbContext<LocalizationDbContext>(config => DbContextConfigurer.Configure(config)) // Contexto de localização
                .AddTnfDbContext<SettingDbContext>(config => DbContextConfigurer.Configure(config));     // Contexto de Settings

            // Configura o uso do Dapper
            // Por default procura o mapeamentos nesse assembly
            // Por default usa SqlServer
            services.AddTnfDapper(options =>
            {
                options.MapperAssemblies.Add(typeof(CustomerMapper).Assembly);
            });

            // Configura o uso do AutoMappper
            services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<CustomerProfile>();
                config.AddProfile<EmployeeProfile>();
            });

            // Habilita a localização do Tnf pelo banco de dados
            services.AddTnfLocalizationManagement();

            return services;
        }
    }
}