using Dapper.Infra.Context;
using Dapper.Infra.Mappers;
using Dapper.Infra.Mappers.DapperMappers;
using Dapper.Infra.Repositories;
using Tnf.Dapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services)
        {
            // Configura o uso do Dapper registrando os contextos que serão
            // usados pela aplicação
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<PurchaseOrderContext>(config => DbContextConfigurer.Configure(config))
                .AddTnfDapper(options =>
                {
                    options.MapperAssemblies.Add(typeof(CustomerMapper).Assembly);
                    options.DbType = DapperDbType.SqlServer;
                });

            services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<PurchaseOrderProfile>();
            });

            services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();

            return services;
        }
    }
}
