using System.Runtime.Serialization;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car;

public class CarCommandUpdate : ICommand<CarResult>, ITransactionCommand
{
    [IgnoreDataMember]
    public Guid? Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public Guid StoreId { get; set; }
}


public class CarCommandUpdateAdmin : CarCommandUpdate, IPermissionRequiredCommand
{
    public bool MustBeAdmin { get; set; }
}
