using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using System;
using System.Threading.Tasks;

namespace SuperMarket.Backoffice.Sales.Infra
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            Task.Factory.StartNew(() =>
            {
                var context = provider.GetRequiredService<SalesContext>();

                context.Database.Migrate();
            });
        }
    }
}
