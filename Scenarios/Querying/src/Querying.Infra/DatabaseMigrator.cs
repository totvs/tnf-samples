using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Querying.Infra.Context;

namespace Querying.Infra
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            Task.Factory.StartNew(() =>
            {
                var context = provider.GetRequiredService<PurchaseOrderContext>();
                context.Database.Migrate();
            });
        }
    }
}
