using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Purchase.Update;

public class UpdatePurchaseCommand
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
}

public class UpdatePurchaseResult
{
    public UpdatePurchaseResult(PurchaseDto purchase)
    {
        Purchase = purchase;
    }

    public PurchaseDto Purchase { get; set; }
}