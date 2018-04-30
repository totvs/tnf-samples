using Security.Infra.Oracle.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Security.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Security.Infra.Context;

namespace Security.Infra.Oracle
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOracleDependency(this IServiceCollection services)
        {
            services
                .AddInfraDependency()
                .AddTnfDbContext<CrudDbContext, OracleCrudDbContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.DbContextOptions.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                        config.UseLoggerFactory();
                    }

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseOracle(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseOracle(config.ConnectionString);
                });

            DevartOracleSettings.SetDefaultSettings();

            return services;
        }
    }
}
