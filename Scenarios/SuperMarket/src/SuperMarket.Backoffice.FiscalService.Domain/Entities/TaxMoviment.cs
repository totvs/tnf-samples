using System;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace SuperMarket.Backoffice.FiscalService.Domain.Entities
{
    public partial class TaxMoviment : Entity<Guid>
    {
        public static Builder New(INotificationHandler notification)
            => new Builder(notification);

        public Guid PurchaseOrderId { get; private set; }
        public decimal PurchaseOrderBaseValue { get; private set; }
        public decimal PurchaseOrderDiscount { get; private set; }
        public int Percentage { get; private set; }
        public decimal Tax { get; private set; }
        public decimal PurchaseOrderTotalValue { get; private set; }

        internal void RecalculateTaxTotalValue()
        {
            Tax = (Percentage / 100.0m);

            PurchaseOrderTotalValue = (PurchaseOrderBaseValue - PurchaseOrderDiscount) + Tax;
        }
    }
}
