using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;
using Transactional.Domain.Entities;

namespace Transactional.Infra.Context
{
    public class OrderContext : TnfDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
