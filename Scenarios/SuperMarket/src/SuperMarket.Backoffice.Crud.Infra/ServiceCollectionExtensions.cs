using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Contexts;
using SuperMarket.Backoffice.Crud.Infra.Repositories;
using SuperMarket.Backoffice.Crud.Infra.Repositories.Interfaces;
using System;
using Tnf.Repositories;

namespace SuperMarket.Backoffice.Crud.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCrudInfraDependency(this IServiceCollection services)
        {
            services
                .AddMapperDependency()
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<CrudContext>((config) =>
                {
                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlServer(config.ConnectionString);
                });

            services.AddTransient<IPriceTableRepository, ProductRepository>();
            services.AddTransient<IRepository<Product, Guid>, ProductRepository>();

            return services;
        }
    }
}
