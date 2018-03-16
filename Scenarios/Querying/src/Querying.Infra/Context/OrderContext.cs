using Microsoft.EntityFrameworkCore;
using Querying.Infra.Entities;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Querying.Infra.Context
{
    public class OrderContext : TnfDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Importante o construtor do contexto receber as opções com o tipo generico definido: DbContextOptions<TDbContext>
        public OrderContext(DbContextOptions<OrderContext> options, ITnfSession session)
            : base(options, session)
        {
        }
    }
}
