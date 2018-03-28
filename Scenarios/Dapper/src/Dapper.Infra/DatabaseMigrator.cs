using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dapper.Infra.Context;
using Tnf.Repositories.Uow;

namespace Dapper.Infra
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            var uowManager = provider.GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin())
            {
                var context = provider.GetRequiredService<PurchaseOrderContext>();

                context.Database.Migrate();
            }
        }
    }
}
