using Microsoft.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts
{
    public static class DbContextConfigurer
    {
        public static void Configure<TContext>(DbContextOptionsBuilder<TContext> builder, string connectionString)
            where TContext : DbContext
        {
            builder.UseSqlServer(connectionString);
        }
    }
}
