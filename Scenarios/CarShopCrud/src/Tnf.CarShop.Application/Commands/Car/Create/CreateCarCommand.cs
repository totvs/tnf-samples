namespace Tnf.CarShop.Application.Commands.Car.Create;

public sealed record CreateCarCommand
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
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