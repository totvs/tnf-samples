using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Car.Create;

public sealed record CarCommand
{
    public Guid? Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
}

public class CarResult
{
    public CarResult(Guid createdCarId, bool success)
    {
        CarId = createdCarId;
        Success = success;
    }

    public CarResult(CarDto carDto)
    {
        CarDto = carDto;
    }

    public CarDto CarDto { get; set; }
    public Guid CarId { get; set; }
    public bool Success { get; set; }
}