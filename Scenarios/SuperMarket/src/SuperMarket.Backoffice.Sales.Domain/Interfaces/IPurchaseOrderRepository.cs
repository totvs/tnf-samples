using SuperMarket.Backoffice.Sales.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Repositories;

namespace SuperMarket.Backoffice.Sales.Domain.Interfaces
{
    public interface IPurchaseOrderRepository : IRepository
    {
        Task<PurchaseOrder> GetPurchaseOrder(Guid id);
        Task<PurchaseOrder> Insert(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder> Update(PurchaseOrder purchaseOrder);
        Task DeletePurchaseOrder(Guid purchaseOrderId);
    }
}
