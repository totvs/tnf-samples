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
    public class PurchaseOrderRepository : EfCoreRepositoryBase<PurchaseOrderContext, PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(IDbContextProvider<PurchaseOrderContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<PurchaseOrder> GetPurchaseOrder(RequestDto request)
        {
            if (!request.GetFields().Any())
                request.Fields = "Id, Date, TotalValue";

            // Para carregar atributos específicos do objeto que será retornado
            // no método Get do repositório do TNF, defina o valor para o campo
            // Fields separados por virgula.
            return base.GetAsync(request);
        }

        /// <summary>
        /// Exemplo de query 1 x N feita através do campo expandables do TNF
        /// que irá carregar o relacionamento "Customer" da entidade de Order
        /// </summary>
        public async Task<Customer> GetCustomerFromPurchaseOrder(RequestDto request)
        {
            if (!request.GetExpandablesFields().Contains("Customer"))
                request.Expand = "Customer";

            // Para carregar relacionamentos específicos do objeto que será retornado
            // no método Get do repositório do TNF, defina o valor para o campo
            // Expand separados por vírgula, quando existir mais de um.
            var order = await base.GetAsync(request);

            return order.Customer;
        }

        /// <summary>
        /// Exemplo de query 1 x N feita de forma explicíta
        /// </summary>
        public async Task<Customer> GetCustomerFromOrderExplicit(int orderId)
        {
            // Para a tabela de Orders
            // Incluo a referência da tabela de Customer
            // Filtrando pelo Id da Order
            var customer = await Context.PurchaseOrders
                .Include(i => i.Customer)    // Inclui o relacionamento
                .Where(w => w.Id == orderId)
                .Select(s => s.Customer)
                .FirstOrDefaultAsync();

            return customer;
        }

        /// <summary>
        /// Query sample N X N and grouping
        /// </summary>
        public async Task<SumarizedPurchaseOrder> GetSumarizedPurchaseOrderFromProduct(SumarizedPurchaseOrderRequestAllDto param)
        {
            // Para a tabela de ProductOrder
            // Incluo a referência da tabela product e order
            // Filtrando para data passada por parâmetro
            var baseQuery = Context.PurchaseOrderProducts
                .Include(i => i.Product)
                .Include(i => i.Order)
                .Where(w => w.Order.Date == param.Date.Date)
                .Select(productOrder => productOrder);

            // Agrupo pelo Id do produto e sua descrição
            var sumarizedByProduct = (from productOrders in baseQuery
                                      group productOrders by new { productOrders.Product.Id, productOrders.Product.Description } into productGroup
                                      select new
                                      {
                                          ProductId = productGroup.Key.Id,
                                          ProductDescription = productGroup.Key.Description,
                                          Amount = productGroup.Sum(s => s.Amount),
                                          TotalValue = productGroup.Sum(s => s.Amount * s.UnitValue)
                                      });

            var sumarized = new SumarizedPurchaseOrder()
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
