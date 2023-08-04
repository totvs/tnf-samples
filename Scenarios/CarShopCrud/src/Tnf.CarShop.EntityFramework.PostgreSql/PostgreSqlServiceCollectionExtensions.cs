using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tnf.CarShop.EntityFrameworkCore.PostgreSql
{
    public static class PostgreSqlServiceCollectionExtensions
    {
        public static IServiceCollection AddEFCorePostgreSql(this IServiceCollection services)
        {
            //services.AddEFCore();

            services.AddTnfDbContext<CarShopDbContext, PostgreSqlCarShopDbContext>(config =>
            {
                config.DbContextOptions.UseNpgsql(config.ConnectionString);
            });

            return services;
        }
    }
}
