using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;
using Transactional.Domain.Entities;

namespace Transactional.Infra.Context
{
    public class PurchaseOrderContext : TnfDbContext
    {
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderProduct> PurchaseOrderProducts { get; set; }

        public PurchaseOrderContext(DbContextOptions<PurchaseOrderContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
