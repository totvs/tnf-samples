using System;

namespace SuperMarket.Backoffice.Sales.Infra.Pocos
{
    public class PurchaseOrderProductPoco
    {
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrderPoco PurchaseOrder { get; set; }

        public Guid ProductId { get; set; }

        public decimal UnitValue { get; set; }
        public int Quantity { get; set; }
    }
}
