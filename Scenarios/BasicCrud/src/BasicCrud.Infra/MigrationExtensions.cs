using System;
using System.Threading.Tasks;
using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Infra
{
    public static class MigrationExtensions
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            Task.Factory.StartNew(() =>
            {
                var context = provider.GetRequiredService<CrudDbContext>();
                context.Database.Migrate();
            });
        }
    }
}
