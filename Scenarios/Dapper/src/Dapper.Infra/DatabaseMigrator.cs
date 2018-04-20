using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dapper.Infra.Context;
using System.Threading.Tasks;

namespace Dapper.Infra
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
