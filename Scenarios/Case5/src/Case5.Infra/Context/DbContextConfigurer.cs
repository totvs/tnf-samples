using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Configuration;

namespace Case5.Infra.Context
{
    public static class DbContextConfigurer
    {
        /// <summary>
        /// Configura o uso do dbcontext para Oracle e faz o reuso da conexão existente em transações aninhadas
        /// </summary>
        public static void Configure<TDbContext>(TnfDbContextConfiguration<TDbContext> config)
            where TDbContext : DbContext
        {
            if (config.ExistingConnection != null)
                config.DbContextOptions.UseOracle(config.ExistingConnection);
            else
                config.DbContextOptions.UseOracle(config.ConnectionString);
        }
    }
}