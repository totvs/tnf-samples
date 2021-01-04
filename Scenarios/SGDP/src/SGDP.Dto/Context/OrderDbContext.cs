using Microsoft.EntityFrameworkCore;

using SGDP.Domain.Entities;

using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;
using Tnf.Sgdp.EntityFrameworkCore;

namespace SGDP.Infra.Context
{
    public class OrderDbContext : TnfDbContext, ISgdpLogBufferDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<TnfSgdpLogEntry> TnfSgdpLogBuffer { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options, ITnfSession session) : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(b =>
            {
                b.HasKey(c => c.Id);

                b.Property(c => c.Cpf).IsRequired(false);
                b.Property(c => c.Rg).IsRequired(false);
                b.Property(c => c.Email).IsRequired(false);
            });
        }
    }
}
