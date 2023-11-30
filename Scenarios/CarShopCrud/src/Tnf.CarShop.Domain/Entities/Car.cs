using Tnf.CarShop.Domain.Dtos;
using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Car : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{
    public Guid Id { get; set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public decimal Price { get; private set; }
    public Guid TenantId { get; set; }

    public Guid StoreId { get; set; }
    public Store Store { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public Car(string brand, string model, int year, decimal price, Guid storeId)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        StoreId = storeId;
    }    

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            return;

        Price = newPrice;
    }

    public void UpdateBrand(string brand)
    {
        if (brand.IsNullOrEmpty())
            return;

        Brand = brand;
    }

    public void UpdateModel(string model)
    {
        if (model.IsNullOrEmpty())
            return;

        Model = model;
    }

    public void UpdateYear(int year)
    {
        if (year <= 0)
            return;

        Year = year;
    }

    public CarDto ToDto()
    {
        return new CarDto(
            Id,
            Brand,
            Model,
            Year,
            Price,
            StoreId);
    }
}
