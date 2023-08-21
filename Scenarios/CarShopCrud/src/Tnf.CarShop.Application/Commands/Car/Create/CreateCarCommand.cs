namespace Tnf.CarShop.Application.Commands.Car.Create;

public sealed record CreateCarCommand
{
    public CreateCarCommand(string brand, string model, int year, decimal price, Guid tenantId)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        TenantId = tenantId;
    }

    public CreateCarCommand(Guid id, string brand, string model, int year, decimal price)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
    }

    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public Guid TenantId { get; set; }
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