namespace Tnf.CarShop.Application.Commands.Store.Delete;

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
