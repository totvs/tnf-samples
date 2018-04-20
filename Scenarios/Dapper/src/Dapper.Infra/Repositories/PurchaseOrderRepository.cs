using Dapper.Infra.Context;
using Dapper.Infra.Dto;
using Dapper.Infra.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dapper.Repositories;
using Tnf.Dto;
using Tnf.Repositories;

namespace Dapper.Infra.Repositories
{
    public class PurchaseOrderRepository : DapperEfRepositoryBase<PurchaseOrderContext, PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(IActiveTransactionProvider activeTransactionProvider)
            : base(activeTransactionProvider)
        {
        }

        public Task<PurchaseOrder> GetPurchaseOrder(DefaultRequestDto request)
        {
            return base.GetAsync(request);
        }

        /// <summary>
        /// Exemplo de query 1 x N feita de forma explicíta
        /// </summary>
        public async Task<Customer> GetCustomerFromPurchaseOrder(int orderId)
        {
            var query = await QueryAsync<Customer>(@"
                SELECT customer.* 
                FROM PurchaseOrders purchaseOrder 
                    INNER JOIN Customers customer ON purchaseOrder.CustomerId = customer.Id
                WHERE purchaseOrder.Id = @Id", 
                new { Id = orderId });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Query sample N X N and grouping
        /// </summary>
        public async Task<SumarizedPurchaseOrder> GetSumarizedPurchaseOrderFromDate(DateTime date)
        {
            // Para a tabela de PurchaseOrderProducts
            // Incluo a referência da tabela product e purchaseOrder
            // Filtrando para data passada por parâmetro
            // Agrupo pelo Id do produto e sua descrição
            var purchaseOrderProductUnits = await QueryAsync<SumarizedProduct>(@"
                SELECT product.Id AS Id, 
                       product.Description AS Description, 
                       SUM(purchaseOrderProduct.Quantity) AS Quantity, 
                       SUM(purchaseOrderProduct.Quantity * purchaseOrderProduct.UnitValue) AS TotalValue
                FROM PurchaseOrderProducts purchaseOrderProduct
                    INNER JOIN Products product ON product.Id = purchaseOrderProduct.ProductId
                    INNER JOIN PurchaseOrders purchaseOrder ON purchaseOrder.Id = purchaseOrderProduct.PurchaseOrderId
                WHERE purchaseOrder.Date = @Date
                GROUP BY product.Id, product.Description", 
                new { Date = date });

            var sumarized = new SumarizedPurchaseOrder()
            {
                Date = date,
                TotalQuantity = purchaseOrderProductUnits.Sum(s => s.Quantity),
                TotalValue = purchaseOrderProductUnits.Sum(s => s.TotalValue),
                Products = purchaseOrderProductUnits
            };

            return sumarized;
        }

        public Task<IListDto<PurchaseOrderDto>> GetAllPurchaseOrders(SumarizedPurchaseOrderRequestAllDto param)
        {
            return param.Date == DateTime.MinValue || param.Date == null ?
                GetAllAsync<PurchaseOrderDto>(param) :
                GetAllAsync<PurchaseOrderDto>(param, purchaseOrder => purchaseOrder.Date == param.Date.Value);
        }
    }
}
