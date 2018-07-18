using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tnf.EntityFrameworkCore.Configuration;

namespace Querying.Infra.Context
{
    public static class DbContextConfigurer
    {
        /// <summary>
        /// Configura o uso do dbcontext para sql server e faz o reuso da connexão existente em transações aninhadas
        /// </summary>
        public static void Configure<TDbContext>(TnfDbContextConfiguration<TDbContext> config)
            where TDbContext : DbContext
        {
            if (Constants.IsDevelopment())
            {
                config.DbContextOptions.EnableSensitiveDataLogging();
                config.DbContextOptions.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.QueryClientEvaluationWarning));
                config.UseLoggerFactory();
            }

            if (config.ExistingConnection != null)
                config.DbContextOptions.UseSqlServer(config.ExistingConnection);
            else
                config.DbContextOptions.UseSqlServer(config.ConnectionString);
        }
    }
}
