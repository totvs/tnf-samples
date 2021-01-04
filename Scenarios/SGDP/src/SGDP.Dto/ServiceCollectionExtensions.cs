using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SGDP.Domain.Interfaces.Repositories;
using SGDP.Domain;
using SGDP.Infra.Repositories;
using SGDP.Infra.Context;

namespace SGDP.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgreSQLDependency(this IServiceCollection services)
        {
            services
                .AddTnfEntityFrameworkCore()    // Configura o uso do EntityFrameworkCore registrando os contextos que serão usados pela aplicação
                .AddMapperDependency();         // Configura o uso do AutoMappper

            // Registro dos repositórios
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();

            services
                .AddTnfDbContext<OrderDbContext, PostgreSQLCrudDbContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.UseLoggerFactory();
                    }

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UsePostgreSql(config.ExistingConnection);
                    else
                        config.DbContextOptions.UsePostgreSql(config.ConnectionString);
                });

            return services;
        }
    }
}
