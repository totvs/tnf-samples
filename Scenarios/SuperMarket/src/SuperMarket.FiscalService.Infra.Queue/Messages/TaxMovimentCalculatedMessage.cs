using System;
using Tnf.Bus.Client;

namespace SuperMarket.Backoffice.Sales.Infra.Queue.Messages
{
    public class TaxMovimentCalculatedMessage : Message
    {
        public Guid PurchaseOrderId { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalValue { get; set; }
    }
}
