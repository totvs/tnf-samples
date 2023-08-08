namespace Tnf.CarShop.Application.Dtos;

public class CarDto
{
    public CarDto(Guid id, string brand, string model, int year, decimal price, DealerDto? dealer, CustomerDto? owner)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        Dealer = dealer;
        Owner = owner;
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
    public DealerDto? Dealer { get; set; }
    public CustomerDto? Owner { get; set; }
}