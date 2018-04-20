using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.FiscalService.Infra.Contexts;
using System;
using System.Threading.Tasks;

namespace SuperMarket.FiscalService.Infra
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            Task.Factory.StartNew(() =>
            {
                var context = provider.GetRequiredService<FiscalContext>();

                context.Database.Migrate();
            });
        }
    }
}
