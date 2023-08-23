using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Car.Update;

public class UpdateCarCommand
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
}

public class UpdateCarResult
{

    public UpdateCarResult(CarDto car)
    {
        Car = car;
    }

    public CarDto Car { get; set; }
}
