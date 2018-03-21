namespace SuperMarket.Backoffice.Sales.Domain.Entities
{
    public partial class PurchaseOrder
    {
        public enum PurchaseOrderStatus
        {
            Processing = 1,
            Completed = 2
        }

        public enum Error
        {
            ProductsThatAreNotInThePriceTable,
            PurchaseOrderLineMustHaveValidQuantity
        }
    }
}
