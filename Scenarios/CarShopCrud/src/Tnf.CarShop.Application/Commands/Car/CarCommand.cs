namespace Tnf.CarShop.Host.Commands.Car.Create;

public class CarCommand
{
    public Guid Id { get; set; }
    public string Brand { get; }
    public string Model { get; }
    public int Year { get; }
    public decimal Price { get; }
    public Guid? DealerId { get; }
    public Guid? OwnerId { get; }
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