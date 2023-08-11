using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public record Purchase : IHasCreationTime, IMustHaveTenant
{
    public Guid Id { get; private set; }
    public Guid CarId { get; }
    public Guid CustomerId { get; }
    public decimal Price { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    
    public DateTime CreationTime { get; set; }
    public Guid TenantId { get; set; }

    public Customer Customer { get; private set; }
    public Car Car { get; private set; }
    public Store Store { get; set; }

    public void UpdateCustomer(Customer newCustomer)
    {
        if (newCustomer != null) 
            Customer = newCustomer;
    }


    public void UpdateCar(Car newCar)
    {
        if (newCar != null)
        {
            Car = newCar;
            Price = newCar.Price;
        }
    }

    public void CompletePurchase(Customer customer, Car car, Store store)
    {
        Customer = customer;
        Car = car;
        Store = store;
    }
}