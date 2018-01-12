using Case2.Infra.Context;
using Case2.Infra.Dapper;
using Case2.Infra.Mapper;
using Tnf.Localization.EntityFrameworkCore;
using Tnf.Settings.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfraDependency(this IServiceCollection services)
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
                .AddTnfDbContext<TnfLocalizationDbContext>(config => DbContextConfigurer.Configure(config))
                .AddTnfDbContext<TnfSettingsDbContext>(config => DbContextConfigurer.Configure(config));

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
        }
    }
}
