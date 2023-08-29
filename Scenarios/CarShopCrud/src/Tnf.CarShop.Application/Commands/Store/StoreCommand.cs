using Tnf.CarShop.Domain.Dtos;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommand : ICommand<StoreResult>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Cnpj { get; set; }
}

public class StoreResult
{
    public StoreDto StoreDto { get; set; }
}
