using Tnf.CarShop.Domain.Dtos;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car;

//Implementar um ICommand<TResult> é opcional, porém uma boa prática por convenção!
public class CarCommand : ICommand<CarResult>
{
    public Guid? Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public Guid StoreId { get; set; }
}

public class CarResult
{
    public CarDto CarDto { get; set; }
}
