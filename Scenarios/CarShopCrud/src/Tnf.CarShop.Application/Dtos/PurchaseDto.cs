namespace Tnf.CarShop.Application.Dtos;

public sealed record PurchaseDto
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

    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public CustomerDto Customer { get; set; }
    public CarDto Car { get; set; }
}