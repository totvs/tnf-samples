using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.Backoffice.Sales.Application.Tests.Mocks
{
    public class PurchaseOrderRepositoryMock : IPurchaseOrderRepository
    {
        private readonly PurchaseOrderServiceMockManager _manager;

        public PurchaseOrderRepositoryMock(PurchaseOrderServiceMockManager manager)
        {
            _manager = manager;
        }

        public Task<PurchaseOrder> GetPurchaseOrder(Guid id)
            => _manager.List.FirstOrDefault(p => p.Id == id).AsTask();

        public Task<PurchaseOrder> Insert(PurchaseOrder purchaseOrder)
        {
            _manager.List.Add(purchaseOrder);

            return purchaseOrder.AsTask();
        }

        public Task<PurchaseOrder> Update(PurchaseOrder purchaseOrder)
        {
            _manager.List.RemoveAll(c => c.Id == purchaseOrder.Id);
            _manager.List.Add(purchaseOrder);

            return purchaseOrder.AsTask();
        }
    }
}
