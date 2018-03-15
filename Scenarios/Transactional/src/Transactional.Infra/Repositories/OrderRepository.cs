using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using Transactional.Domain.Entities;
using Transactional.Domain.Interfaces;
using Transactional.Infra.Context;

namespace Transactional.Infra.Repositories
{
    public class OrderRepository : EfCoreRepositoryBase<OrderContext, Order>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<OrderContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Order> CheckDuplicateOrder(Order order)
        {
            var duplicateOrder = await FirstOrDefaultAsync(w => w.ClientId == order.ClientId && w.Data == order.Data);
            return duplicateOrder;
        }

        public async Task<Order> CreateNewOrder(Order order)
        {
            order.Id = await base.InsertAndGetIdAsync(order);
            return order;
        }

        public List<Order> GetAllOrders()
        {
            return base.GetAllIncluding(i => i.Products).ToList();
        }
    }
}
