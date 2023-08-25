namespace Tnf.CarShop.Domain.Dtos;

public class PurchaseDto
{
    public PurchaseDto()
    {
    }

    public PurchaseDto(Guid id, DateTime purchaseDate)
    {
        Id = id;
        PurchaseDate = purchaseDate;
    }

    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public CustomerDto Customer { get; set; }
    public CarDto Car { get; set; }
    public StoreDto Store { get; set; }
}
