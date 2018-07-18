using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Contexts;
using SuperMarket.Backoffice.Crud.Infra.Repositories;
using SuperMarket.Backoffice.Crud.Infra.Repositories.Interfaces;
using Tnf.Caching.Redis;
using Tnf.Repositories;

namespace SuperMarket.Backoffice.Crud.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCrudInfraDependency(this IServiceCollection services, int databaseIndex, string redisConnectionString)
        {
            services
                .AddMapperDependency()
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<CrudContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.DbContextOptions.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.QueryClientEvaluationWarning));
                        config.UseLoggerFactory();
                    }

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlServer(config.ConnectionString);
                });

            services.AddTransient<IPriceTableRepository, ProductRepository>();
            services.AddTransient<IRepository<Product>, ProductRepository>();

            services.AddTnfRedisCache(builder => builder

               // Nome para o qual o cache será registrado no DI
               .UseDefaultName("Default")

               // Para customizar a serialização implemente a interface Tnf.Caching.Redis.IRedisSerializer
               // e passe a instancia do seu serializador utilizando o método .UseSerializer()
               .UseJsonSerializer()
               .UseCacheOptions(new CacheOptions()
               {
                   LogDeletedKeys = true,                // Exibir no log quando uma key for deletada
               })
               .UseDatabase(databaseIndex)                     // Redis Database Id
               .UseConnectionString(redisConnectionString));   // Redis Connection String

            return services;
        }
    }
}
