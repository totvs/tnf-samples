namespace Tnf.CarShop.Application.Dtos;

public class CarDto
{
    public CarDto(Guid id, string brand, string model, int year, decimal price, Guid? dealerId, Guid? ownerId)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        DealerId = dealerId;
        OwnerId = ownerId;
    }

    public CarDto(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public Guid? DealerId { get; set; }
    public Guid? OwnerId { get; set; }

}