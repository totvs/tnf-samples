using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Infra.AutoMapperProfiles;
using SuperMarket.Backoffice.Crud.Infra.Contexts;

namespace SuperMarket.Backoffice.Crud.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCrudInfraDependency(this IServiceCollection services)
        {
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<CrudContext>((config) =>
                {
                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlServer(config.ConnectionString);
                });

            services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<InfraToDtoProfile>();
            });

            return services;
        }
    }
}
