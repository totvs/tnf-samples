using Tnf.CarShop.Domain.Dtos;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car;

public class CarCommandCreate : ICommand<CarResult>, ITransactionCommand
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public Guid StoreId { get; set; }
}

public class CarCommandCreateAdmin : CarCommandCreate, IPermissionRequiredCommand
{
    public bool MustBeAdmin { get; set; }
}
