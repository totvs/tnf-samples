using System;
using BasicCrud.Infra.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tnf.Repositories.Uow;

namespace BasicCrud.Infra.SqlServer
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            var uowManager = provider.GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin())
            {
                var context = provider.GetRequiredService<BasicCrudDbContext>();

                context.Database.Migrate();
            }
        }
    }
}
