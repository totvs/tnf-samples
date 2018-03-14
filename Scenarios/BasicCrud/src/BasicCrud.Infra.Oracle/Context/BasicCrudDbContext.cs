using BasicCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.Oracle.Context
{
    public class BasicCrudDbContext : TnfDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        // Importante o construtor do contexto receber as opções com o tipo generico definido: DbContextOptions<TDbContext>
        public BasicCrudDbContext(DbContextOptions<BasicCrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(m =>
            {
                m.HasKey(k => k.Id);
                m.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<Product>(m =>
            {
                m.HasKey(k => k.Id);
                m.Property(p => p.Description).IsRequired();
                m.Property(p => p.Value).IsRequired();
            });
        }
    }
}