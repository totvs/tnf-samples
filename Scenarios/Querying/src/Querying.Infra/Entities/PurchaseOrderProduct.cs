namespace Querying.Infra.Entities
{
    public class PurchaseOrderProduct : IEntity
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitValue { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public PurchaseOrder Order { get; set; }

        public PurchaseOrderProduct() { }

        public PurchaseOrderProduct(int purchaseOrderId, int productId, int quantity, decimal unitValue)
        {
            PurchaseOrderId = purchaseOrderId;
            ProductId = productId;
            UnitValue = unitValue;
            Quantity = quantity;
        }
    }
}
