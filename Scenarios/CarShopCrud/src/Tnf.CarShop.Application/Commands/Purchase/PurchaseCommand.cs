namespace Tnf.CarShop.Host.Commands.Purchase
{
    public class PurchaseCommand
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
    }

    public class PurchaseResult
    {
        public PurchaseResult(Guid createdPurchaseId, bool success)
        {
            PurchaseId = createdPurchaseId;
            Success = success;
        }

        public Guid PurchaseId { get; set; }
        public bool Success { get; set; }
    }
}