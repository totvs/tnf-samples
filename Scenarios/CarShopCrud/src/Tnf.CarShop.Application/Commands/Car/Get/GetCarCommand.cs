using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Car.Get;

public class GetCarCommand
{
    public Guid? CarId { get; set; }
}

public class GetCarResult
{
    public GetCarResult(CarDto car)
    {
        Car = car;
    }

    public GetCarResult(List<CarDto> cars)
    {
        Cars = cars;
    }

    public CarDto Car { get; set; }

    public List<CarDto> Cars { get; set; }
}