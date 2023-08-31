using Tnf.CarShop.Domain.Dtos;
using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Purchase : IHasCreationTime, IMustHaveTenant
{
    public Guid Id { get; set;  }
    public Guid CarId { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid StoreId { get; private set; }
    public decimal Price { get; private set; }
    public DateTime PurchaseDate { get; private set; }

    public Customer Customer { get; private set; }
    public Car Car { get; private set; }
    public Store Store { get; private set; }

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
        CustomerId = newCustomer.Id;
    }

    public void UpdateCar(Car newCar)
    {
        CarId = newCar.Id;
        Price = newCar.Price;
    }

    public void UpdateStore(Store store)
    {
        StoreId = store.Id;
    }

    public PurchaseDto ToDto()
    {
        var dto = new PurchaseDto(Id, PurchaseDate);
        //dto.Car = Car.ToDto();
        //dto.Customer = Customer.ToDto();
        //dto.Store = Store.ToDto();

        return dto;
    }
}
