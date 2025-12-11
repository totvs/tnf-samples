using System.Collections.Generic;
using System.Threading.Tasks;
using Transactional.Domain.Entities;

namespace Transactional.Domain.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task<PurchaseOrder> CreateNewPurchaseOrder(PurchaseOrder purchaseOrder);
        List<PurchaseOrder> GetAllPurchaseOrders();
        Task DeleteAsync(int id);
    }
}