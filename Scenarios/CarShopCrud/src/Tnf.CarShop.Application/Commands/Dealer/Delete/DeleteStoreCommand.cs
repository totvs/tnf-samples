namespace Tnf.CarShop.Application.Commands.Dealer.Delete;

public class DeleteStoreCommand
{
    public Guid StoreId { get; set; }
}

public class DeleteStoreResult
{
    public DeleteStoreResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
}