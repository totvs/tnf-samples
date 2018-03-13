using BasicCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.SqLite
{
    public class CustomerDbContext : TnfDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options, ITnfSession session) 
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
        }
    }
}