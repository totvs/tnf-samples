﻿using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Purchase : IHasCreationTime, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid CarId { get; }
    public Guid CustomerId { get; }
    public Guid StoreId { get; set; }
    public decimal Price { get; private set; }
    public DateTime PurchaseDate { get; private set; }

    public Customer Customer { get; private set; }
    public Car Car { get; private set; }
    public Store Store { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid TenantId { get; set; }

    public Purchase(Guid carId, Guid customerId, decimal price, DateTime purchaseDate, Guid storeId)
    {
        CarId = carId;
        CustomerId = customerId;
        Price = price;
        PurchaseDate = purchaseDate;
        StoreId = storeId;
    }

    public void UpdatePurchaseDate(DateTime? purchaseDate)
    {
        if (purchaseDate.HasValue)
        {
            PurchaseDate = purchaseDate.Value;
            return;
        }

        PurchaseDate = DateTime.Now;
    }

    public void UpdateCustomer(Customer newCustomer)
    {
        Customer = newCustomer;
    }

    public void UpdateCar(Car newCar)
    {
        Car = newCar;
        Price = newCar.Price;
    }

    public void UpdateStore(Store store)
    {
        Store = store;
    }
}
