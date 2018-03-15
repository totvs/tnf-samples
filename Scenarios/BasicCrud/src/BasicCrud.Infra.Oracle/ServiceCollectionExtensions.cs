using BasicCrud.Domain.Interfaces.Repositories;
using BasicCrud.Infra.ReadInterfaces;
using BasicCrud.Infra.Oracle.Context;
using BasicCrud.Infra.Oracle.Repositories;
using BasicCrud.Infra.Oracle.Repositories.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Infra.Oracle
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerDependency(this IServiceCollection services)
        {
            services
                .AddInfraDependency()
                .AddTnfDbContext<BasicCrudDbContext>((config) =>
                {
                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseOracle(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseOracle(config.ConnectionString);
                });


            // Registro dos repositórios
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductReadRepository, ProductReadRepository>();

            return services;
        }
    }
}
