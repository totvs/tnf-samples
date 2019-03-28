using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProdutoXyz.Infra.Postgres.Context;
using ProdutoXyz.Infra.Context;
using ProdutoXyz.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProdutoXyz.Infra;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgressDependency(this IServiceCollection services)
        {
            services.AddInfraDependency()
                .AddTnfDbContext<CrudDbContext, PostgresCrudDbContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.DbContextOptions.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
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
