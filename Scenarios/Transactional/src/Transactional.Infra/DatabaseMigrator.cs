using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tnf.Repositories.Uow;
using Transactional.Infra.Context;

namespace Transactional.Infra
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            var uowManager = provider.GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin())
            {
                var context = provider.GetRequiredService<OrderContext>();

                context.Database.Migrate();
            }
        }
    }
}
