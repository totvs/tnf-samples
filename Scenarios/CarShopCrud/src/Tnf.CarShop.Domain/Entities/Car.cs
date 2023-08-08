using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public record Car : IHasCreationTime, IHasModificationTime, IMayHaveTenant
{
    protected Car(Customer owner, string brand, string model, Dealer dealer)
    {
        Owner = owner;
        Brand = brand;
        Model = model;
        Dealer = dealer;
    }

    public Car(Guid id, string brand, string model, int year, decimal price)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
    }

    public Guid Id { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public decimal Price { get; private set; }
    private decimal Discount { get; set; }
    public bool IsNew => DateTime.Now.Year - Year <= 1;
    public bool IsOld => DateTime.Now.Year - Year > 20;
    public Dealer Dealer { get; private set; }
    public Customer Owner { get; private set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public Guid? TenantId { get; set; }

    public void ApplyDiscount(decimal percentage)
    {
        Discount = Price * percentage / 100;
        Price -= Discount;
    }

    public decimal GetDiscountedPrice()
    {
        return Price - Discount;
    }

    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }

    public void AssignToDealer(Dealer newDealer)
    {
        Dealer = newDealer;
    }

    public void UpdateBrand(string brand)
    {
        Brand = brand;
    }

    public void UpdateModel(string model)
    {
        Model = model;
    }

    public void UpdateYear(int year)
    {
        Year = year;
    }

    public void AssignToOwner(Customer owner)
    {
        Owner = owner;
    }
}