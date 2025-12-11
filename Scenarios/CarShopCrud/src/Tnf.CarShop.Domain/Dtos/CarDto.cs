namespace Tnf.CarShop.Domain.Dtos;

public class CarDto
{
    public CarDto()
    {
    }

    public CarDto(Guid id, string brand, string model, int year, decimal price, Guid storeId)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        StoreId = storeId;
    }    

    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public Guid StoreId { get; set; }
}
