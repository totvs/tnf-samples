using Microsoft.EntityFrameworkCore;
using SuperMarket.Backoffice.Sales.Infra.Mappers;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace SuperMarket.Backoffice.Sales.Infra.Contexts
{
    public class SalesContext : TnfDbContext
    {
        public DbSet<PurchaseOrderPoco> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderProductPoco> PurchaseOrderProducts { get; set; }

        public SalesContext(DbContextOptions<SalesContext> options, ITnfSession session)
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PurchaseOrderPocoMapper());
            modelBuilder.ApplyConfiguration(new PurchaseOrderProductPocoMapper());
        }
    }
}
