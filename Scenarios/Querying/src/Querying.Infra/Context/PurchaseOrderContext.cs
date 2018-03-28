using Microsoft.EntityFrameworkCore;
using Querying.Infra.Entities;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Querying.Infra.Context
{
    public class PurchaseOrderContext : TnfDbContext
    {
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseOrderProduct> PurchaseOrderProducts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Importante o construtor do contexto receber as opções com o tipo generico definido: DbContextOptions<TDbContext>
        public PurchaseOrderContext(DbContextOptions<PurchaseOrderContext> options, ITnfSession session)
            : base(options, session)
        {
        }
    }
}
