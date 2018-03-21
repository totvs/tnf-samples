using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Sales.Domain.Services
{
    public class PurchaseOrderService : DomainService, IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _repository;

        public PurchaseOrderService(IPurchaseOrderRepository repository, INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            _repository = repository;
        }

        public async Task<PurchaseOrder> NewPurchaseOrder(PurchaseOrder.INewPurchaseOrderBuilder newPurchaseOrderBuilder)
        {
            var purchaseOrder = newPurchaseOrderBuilder.Build();

            if (Notification.HasNotification())
                return purchaseOrder;

            purchaseOrder = await _repository.Save(purchaseOrder);

            return purchaseOrder;
        }

        public async Task<PurchaseOrder> UpdatePurchaseOrder(PurchaseOrder.IUpdatePurchaseOrderBuilder updatePurchaseOrderBuilder)
        {
            var purchaseOrder = updatePurchaseOrderBuilder.Build();

            if (Notification.HasNotification())
                return purchaseOrder;

            purchaseOrder = await _repository.Save(purchaseOrder);

            return purchaseOrder;
        }

        public async Task UpdateTaxPurchaseOrder(Guid purchaseOrderId, decimal tax)
        {
            var purchaseOrder = await _repository.GetPurchaseOrder(purchaseOrderId);

            PurchaseOrder.UpdateTax(purchaseOrder, tax);

            await _repository.Save(purchaseOrder);
        }
    }
}
