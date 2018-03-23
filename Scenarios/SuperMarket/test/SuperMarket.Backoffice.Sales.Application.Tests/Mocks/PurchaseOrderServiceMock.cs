using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Sales.Application.Tests.Mocks
{
    public class PurchaseOrderServiceMock : IPurchaseOrderService
    {
        private readonly PurchaseOrderServiceMockManager _manager;
        private readonly INotificationHandler _notificationHandler;

        public PurchaseOrderServiceMock(PurchaseOrderServiceMockManager manager, INotificationHandler notificationHandler)
        {
            _manager = manager;
            _notificationHandler = notificationHandler;
        }

        public Task<PurchaseOrder> NewPurchaseOrder(PurchaseOrder.INewPurchaseOrderBuilder newPurchaseOrderBuilder)
        {
            var entity = newPurchaseOrderBuilder.Build();

            if (!_notificationHandler.HasNotification())
                _manager.List.Add(entity);

            return entity.AsTask();
        }

        public Task<PurchaseOrder> UpdatePurchaseOrder(PurchaseOrder.IUpdatePurchaseOrderBuilder updatePurchaseOrderBuilder)
        {
            var entity = updatePurchaseOrderBuilder.Build();

            if (!_notificationHandler.HasNotification())
            {
                _manager.List.RemoveAll(c => c.Id == entity.Id);
                _manager.List.Add(entity);
            }

            return entity.AsTask();
        }

        public Task UpdateTaxMoviment(Guid purchaseOrderId, decimal tax, decimal totalValue)
        {
            var entity = _manager.List.SingleOrDefault(p => p.Id == purchaseOrderId);

            entity.UpdateTaxMoviment(tax, totalValue);

            _manager.List.RemoveAll(c => c.Id == purchaseOrderId);
            _manager.List.Add(entity);

            return entity.AsTask();
        }
    }
}
