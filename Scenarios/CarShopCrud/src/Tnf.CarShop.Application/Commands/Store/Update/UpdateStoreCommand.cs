using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Store.Update;

public class UpdateStoreCommand
{
    public UpdateStoreCommand(Guid id, string name, string location, string cnpj)
    {
        Id = id;
        Name = name;
        Location = location;
        Cnpj = cnpj;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Cnpj { get; set; }
}

public class UpdateStoreResult
{
    public UpdateStoreResult(StoreDto store)
    {
        Store = store;
    }

    public StoreDto Store { get; set; }
}