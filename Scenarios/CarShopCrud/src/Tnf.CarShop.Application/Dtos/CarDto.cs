namespace Tnf.CarShop.Application.Dtos;

public sealed record CarDto
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

    public Guid Id { get; init; }
    public string Brand { get; init; }
    public string Model { get; init; }
    public int Year { get; init; }
    public decimal Price { get; init; }
    public DealerDto? Dealer { get; init; }
    public CustomerDto? Owner { get; init; }

}