using Security.Domain.Entities;
using Security.Infra.Context.Builders;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Security.Infra.Context
{
    public abstract class CrudDbContext : TnfDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public CrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session)
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        }
    }
}
