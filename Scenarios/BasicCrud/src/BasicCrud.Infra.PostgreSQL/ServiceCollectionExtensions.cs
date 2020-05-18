using System;
using BasicCrud.Infra.Context;
using BasicCrud.Infra.PostgreSQL.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BasicCrud.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BasicCrud.Infra.PostgreSQL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgreSQLDependency(this IServiceCollection services)
        {
            services
                .AddInfraDependency()
                .AddTnfDbContext<CrudDbContext, PostgreSQLCrudDbContext>((config) =>
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
