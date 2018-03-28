﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.FiscalService.Infra.Contexts;
using System;
using Tnf.Repositories.Uow;

namespace SuperMarket.FiscalService.Infra
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            var uowManager = provider.GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin())
            {
                var context = provider.GetRequiredService<FiscalContext>();

                context.Database.Migrate();
            }
        }
    }
}