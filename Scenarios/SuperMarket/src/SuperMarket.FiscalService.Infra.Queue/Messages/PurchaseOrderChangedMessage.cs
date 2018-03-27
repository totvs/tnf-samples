using System;
using Tnf.Bus.Client;

namespace SuperMarket.Backoffice.Sales.Infra.Queue.Messages
{
    public class PurchaseOrderChangedMessage : Message
    {
        public Guid PurchaseOrderId { get; set; }
        public decimal PurchaseOrderBaseValue { get; set; }
        public decimal PurchaseOrderDiscount { get; set; }
    }
}
