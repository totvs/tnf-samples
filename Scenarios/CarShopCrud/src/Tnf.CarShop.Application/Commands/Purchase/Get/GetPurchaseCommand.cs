using Tnf.CarShop.Domain.Dtos;
using Tnf.Dto;

namespace Tnf.CarShop.Application.Commands.Purchase.Get;

public class GetPurchaseCommand
{
    public Guid? PurchaseId { get; set; }
    public RequestAllDto RequestAllPurchases { get; set; }
}

public class GetPurchaseResult
{
    public GetPurchaseResult(PurchaseDto purchase)
    {
        Purchase = purchase;
    }

    public GetPurchaseResult(IListDto<PurchaseDto> purchases)
    {
        Purchases = purchases;
    }

    public IListDto<PurchaseDto> Purchases { get; set; }
    public PurchaseDto Purchase { get; set; }
}
