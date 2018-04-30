using System;
using System.Threading.Tasks;
using Security.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Infra
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
