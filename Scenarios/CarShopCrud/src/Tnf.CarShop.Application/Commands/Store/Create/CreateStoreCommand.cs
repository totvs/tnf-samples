using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Store.Create;

public class CreateStoreCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public string Location { get; set; }
}

public class CreateStoreResult
{
    public CreateStoreResult(Guid dealerId)
    {
        DealerId = dealerId;
    }

    public Guid DealerId { get; set; }
}