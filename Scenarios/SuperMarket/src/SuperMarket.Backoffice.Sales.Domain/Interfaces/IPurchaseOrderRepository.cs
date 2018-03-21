using SuperMarket.Backoffice.Sales.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SuperMarket.Backoffice.Sales.Domain.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder> GetPurchaseOrder(Guid id);
        Task<PurchaseOrder> Save(PurchaseOrder purchaseOrder);
    }
}
