using Microsoft.EntityFrameworkCore;
using Querying.Infra.Context;
using Querying.Infra.Dto;
using Querying.Infra.Entities;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Querying.Infra.Repositories
{
    public class OrderRepository : EfCoreRepositoryBase<OrderContext, Order>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<OrderContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<Order> GetOrder(RequestDto request)
        {
            return base.GetAsync(request);
        }

        /// <summary>
        /// Exemplo de query 1 x N feita através do campo expandables do TNF
        /// que irá carregar o relacionamento "Customer" da entidade de Order
        /// </summary>
        public async Task<Customer> GetCustomerFromOrder(int orderId)
        {
            var orderRequestDto = new RequestDto(orderId)
            {
                Expand = "Customer"
            };

            var order = await base.GetAsync(orderRequestDto);

            return order.Customer;
        }

        /// <summary>
        /// Exemplo de query 1 x N feita manualmente
        /// </summary>
        public async Task<Customer> GetCustomerFromOrderManual(int orderId)
        {
            var customer = await Context.Orders
                .Include(i => i.Customer)    // Inclui o relacionamento
                .Where(w => w.Id == orderId)
                .Select(s => s.Customer)
                .FirstOrDefaultAsync();

            return customer;
        }

        /// <summary>
        /// Query sample N X N and grouping
        /// </summary>
        public async Task<SumarizedOrder> GetSumarizedOrderFromProduct(SumarizedOrderRequestAllDto param)
        {
            // Usando os extensions methods do System.Linq
            var baseQuery = Context.ProductOrders
                .Include(i => i.Product)
                .Include(i => i.Order)
                .Where(w => w.Order.Date == param.Date.Date)
                .Select(s => s);

            // Utilizando anonymous query
            var sumarizedByProduct = (from productOrders in baseQuery
                                      group productOrders by new { productOrders.Product.Id, productOrders.Product.Description } into productGroup
                                      select new
                                      {
                                          ProductId = productGroup.Key.Id,
                                          ProductDescription = productGroup.Key.Description,
                                          Amount = productGroup.Sum(s => s.Amount),
                                          TotalValue = productGroup.Sum(s => s.Amount * s.UnitValue)
                                      });

            var sumarized = new SumarizedOrder()
            {
                Date = param.Date.Date,
                TotalAmount = await baseQuery.SumAsync(s => s.Amount),
                TotalValue = await sumarizedByProduct.SumAsync(s => s.TotalValue),
                Products = sumarizedByProduct.Select(s => new SumarizedProduct()
                {
                    Id = s.ProductId,
                    Description = s.ProductDescription,
                    Amount = s.Amount,
                    TotalValue = s.TotalValue
                })
            };

            return sumarized;
        }
    }
}
