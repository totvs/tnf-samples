﻿using Tnf.CarShop.Domain.Dtos;
using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Car : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{
    public Guid Id { get; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public decimal Price { get; private set; }
    private decimal Discount { get; set; }
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
        Discount = 0;
    }        

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
