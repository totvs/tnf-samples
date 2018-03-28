using Querying.Infra.Dto;
using Querying.Infra.Entities;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace Querying.Infra.Repositories
{
    public interface IPurchaseOrderRepository : IRepository
    {
        Task<Customer> GetCustomerFromPurchaseOrder(RequestDto request);
        Task<PurchaseOrder> GetPurchaseOrder(RequestDto request);
        Task<SumarizedPurchaseOrder> GetSumarizedPurchaseOrderFromProduct(SumarizedPurchaseOrderRequestAllDto param);
    }
}
