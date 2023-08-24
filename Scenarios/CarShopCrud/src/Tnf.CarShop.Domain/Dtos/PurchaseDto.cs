namespace Tnf.CarShop.Domain.Dtos;

public class PurchaseDto
{
    public PurchaseDto(Guid id, DateTime purchaseDate, CustomerDto customer, CarDto car)
    {
        Id = id;
        PurchaseDate = purchaseDate;
        Customer = customer;
        Car = car;
    }

    public PurchaseDto()
    {
    }

    public PurchaseDto(Guid id, DateTime purchaseDate)
    {
        Id = id;
        PurchaseDate = purchaseDate;
    }

    public PurchaseDto(Guid id, DateTime purchaseDate, Guid carId, Guid customerId, Guid tenantId)
    {
        Id = id;
        PurchaseDate = purchaseDate;

        CarId = carId;
        CustomerId = customerId;
        TenantId = tenantId;
    }

    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public CustomerDto Customer { get; set; }
    public CarDto Car { get; set; }
    public Guid CarId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid TenantId { get; set; }
}
