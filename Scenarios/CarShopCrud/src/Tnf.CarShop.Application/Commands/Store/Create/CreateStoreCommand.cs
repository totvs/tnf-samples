namespace Tnf.CarShop.Application.Commands.Store.Create;

public class CreateStoreCommand
{
    public CreateStoreCommand(string name, string cnpj, string location)
    {
        Name = name;
        Cnpj = cnpj;
        Location = location;
    }

    public CreateStoreCommand()
    {

    }

    public string Name { get; set; }
    public string Cnpj { get; set; }
    public string Location { get; set; }
}

public class CreateStoreResult
{
    public CreateStoreResult(Guid tenantId)
    {
        TenantId = tenantId;
    }

    public Guid TenantId { get; set; }
}
