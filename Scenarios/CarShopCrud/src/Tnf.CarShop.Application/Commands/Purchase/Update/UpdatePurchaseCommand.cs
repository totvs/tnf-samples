using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Purchase.Update;

public class UpdatePurchaseCommand
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Guid StoreId { get; set; }
    public Guid CarId { get; set; }
    public Guid TenantId { get; set; }
    public decimal Price { get; set; }
    public Guid CustomerId { get; set; }
}

public class UpdatePurchaseResult
{
    public UpdatePurchaseResult(PurchaseDto purchase)
    {
        Purchase = purchase;
    }

    public PurchaseDto Purchase { get; set; }
}