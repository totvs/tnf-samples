using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Purchase.Create;

public class CreatePurchaseCommand
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Guid StoreId { get; set; }
    public Guid CarId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid TenantId { get; set; }
    public decimal Price { get; set; }
}

public class CreatePurchaseResult
{
    public CreatePurchaseResult(Guid createdPurchaseId)
    {
        PurchaseId = createdPurchaseId;
    }

    public Guid PurchaseId { get; set; }
}