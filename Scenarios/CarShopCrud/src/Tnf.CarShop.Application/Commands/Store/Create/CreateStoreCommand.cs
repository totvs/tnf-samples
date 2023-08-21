namespace Tnf.CarShop.Application.Commands.Store.Create;

public class CreateStoreCommand
{
    public CreateStoreCommand(Guid id, string name, string cnpj, string location)
    {
        Id = id;
        Name = name;
        Cnpj = cnpj;
        Location = location;
    }

    public CreateStoreCommand(string name, string cnpj, string location)
    {
        Name = name;
        Cnpj = cnpj;
        Location = location;
    }

    public Guid Id { get; set; }
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