namespace Tnf.CarShop.Host.Commands.Purchase.Delete;

public class DeletePurchaseCommand
{
    public Guid PurchaseId { get; set; }
}

public class DeletePurchaseResult
{
    public DeletePurchaseResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
}