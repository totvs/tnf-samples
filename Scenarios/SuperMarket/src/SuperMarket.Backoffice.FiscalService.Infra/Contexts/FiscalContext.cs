using Microsoft.EntityFrameworkCore;
using SuperMarket.Backoffice.FiscalService.Domain.Entities;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace SuperMarket.Backoffice.FiscalService.Infra.Contexts
{
    public class FiscalContext : TnfDbContext
    {
        public DbSet<PurchaseOrderTaxMoviment> PurchaseOrderTaxMoviments { get; set; }

        public FiscalContext(DbContextOptions<FiscalContext> options, ITnfSession session) 
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PurchaseOrderTaxMovimentMapper());
        }
    }
}
