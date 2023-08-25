namespace Tnf.CarShop.Application.Commands.Store.Create;

public class CreateStoreCommand
{
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public string Location { get; set; }
}

public class CreateStoreResult
{
    public CreateStoreResult(Guid storeId)
    {
        StoreId = storeId;
    }

    public Guid StoreId { get; set; }
}
