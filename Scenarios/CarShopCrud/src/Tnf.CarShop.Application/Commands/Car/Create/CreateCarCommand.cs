using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Car.Create;

public sealed record CreateCarCommand
{
    public CarDto Car { get; set; }
}

public class CreateCarResult
{
    public CreateCarResult(Guid createdCarId, bool success)
    {
        CarId = createdCarId;
        Success = success;
    }

    public Guid CarId { get; set; }
    public bool Success { get; set; }
}