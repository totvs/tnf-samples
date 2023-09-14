using Tnf.CarShop.Domain.Dtos;

namespace Tnf.CarShop.Application.Commands.Purchase;
public class PurchaseResult : AdminResult
{
    public PurchaseDto PurchaseDto { get; set; }
}
