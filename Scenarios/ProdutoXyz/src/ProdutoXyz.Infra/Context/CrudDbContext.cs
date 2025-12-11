using ProdutoXyz.Domain.Entities;
using ProdutoXyz.Infra.Context.Builders;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace ProdutoXyz.Infra.Context
{
    public abstract class CrudDbContext : TnfDbContext
    {
        public DbSet<Product> Products { get; set; }

        public CrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session)
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        }
    }
}
