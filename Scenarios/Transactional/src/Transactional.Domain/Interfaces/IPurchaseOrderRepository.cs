using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Repositories;
using Transactional.Domain.Entities;

namespace Transactional.Domain.Interfaces
{
    public interface IPurchaseOrderRepository : IRepository
    {
        Task<PurchaseOrder> CheckDuplicatePurchaseOrder(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder> CreateNewPurchaseOrder(PurchaseOrder purchaseOrder);
        List<PurchaseOrder> GetAllPurchaseOrders();
        Task DeleteAsync(int id);
    }
}
