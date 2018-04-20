using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.FiscalService.Domain;
using SuperMarket.FiscalService.Infra.AutoMapperProfiles;
using SuperMarket.FiscalService.Infra.Contexts;

namespace SuperMarket.FiscalService.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFiscalInfraDependency(this IServiceCollection services)
        {
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<FiscalContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.DbContextOptions.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                        config.UseLoggerFactory();
                    }

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlServer(config.ConnectionString);
                });

            services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<TaxMovimentProfile>();
            });

            return services;
        }
    }
}
