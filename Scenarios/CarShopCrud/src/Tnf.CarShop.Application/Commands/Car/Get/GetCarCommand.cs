using Tnf.CarShop.Domain.Dtos;
using Tnf.Dto;

namespace Tnf.CarShop.Application.Commands.Car.Get;

public class GetCarCommand
{
    public Guid? CarId { get; set; }
    public RequestAllDto RequestAllCars { get; set; }
}

public class GetCarResult
{
    public GetCarResult(CarDto car)
    {
        Car = car;
    }

    public GetCarResult(IListDto<CarDto> cars)
    {
        Cars = cars;
    }

    public CarDto Car { get; set; }

    public IListDto<CarDto> Cars { get; set; }
}
