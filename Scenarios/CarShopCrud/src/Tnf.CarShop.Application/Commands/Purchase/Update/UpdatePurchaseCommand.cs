using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Purchase.Update;

public class UpdatePurchaseCommand
{
    public PurchaseDto Purchase { get; set; }
}

public class UpdatePurchaseResult
{
    public UpdatePurchaseResult(PurchaseDto purchase)
    {
        Purchase = purchase;
    }

    public PurchaseDto Purchase { get; set; }
}