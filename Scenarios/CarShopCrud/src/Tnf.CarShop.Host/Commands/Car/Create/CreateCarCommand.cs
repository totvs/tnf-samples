namespace CarShop.Commands.Car.Create;

public class CreateCarCommand
{
    public Guid Id { get; set; }
    public string Brand { get; }
    public string Model { get; }
    public int Year { get; }
    public decimal Price { get; }
    public Guid DealerId { get; }
    public Guid OwnerId { get; }
}

public class CarResult
{
    public CarResult(Guid createdCarId, bool success)
    {
        CarId = createdCarId;
        Success = success;
    }

    public Guid CarId { get; set; }
    public bool Success { get; set; }
    
}