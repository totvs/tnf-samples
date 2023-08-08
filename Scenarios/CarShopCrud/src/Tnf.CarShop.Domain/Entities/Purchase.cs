﻿using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public record Purchase : IHasCreationTime, IMayHaveTenant
{
    public Guid Id { get; private set; }
    public Guid? TenantId { get; set; }
    public Guid CustomerId { get; private set; }
    public Guid CarId { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreationTime { get; set; }

    public Customer Customer { get; private set; }
    public Car Car { get; private set; }

    protected Purchase() { }

    public Purchase(Customer customer, Car car)
    {
        Customer = customer;
        Car = car;
        Price = car.Price;
        PurchaseDate = DateTime.Now;
    }
    
    public Purchase(Guid id, Customer customer, Car car, DateTime purchaseDate)
    {
        Id = id;
        Customer = customer;
        Car = car;
        Price = car.Price;
        PurchaseDate = purchaseDate;
    }
    
    public void UpdateCustomer(Customer newCustomer)
    {
        if (newCustomer != null)
        {
            Customer = newCustomer;
        }
    }
    

    public void UpdateCar(Car newCar)
    {
        if (newCar != null)
        {
            Car = newCar;
            Price = newCar.Price;
        }
    }

    public void CompletePurchase()
    {
        Car.AssignToOwner(Customer);
    }
}
