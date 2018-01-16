using Case2.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Case2.Infra.Context
{
    public class CustomerDbContext : TnfDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        // Importante o construtor do contexto receber as opções com o tipo generico definido: DbContextOptions<TDbContext>
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options, ITnfSession session)
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(m =>
            {
                m.HasKey(k => k.Id);
                m.Property(p => p.Name).IsRequired();
            });
        }
    }
}
