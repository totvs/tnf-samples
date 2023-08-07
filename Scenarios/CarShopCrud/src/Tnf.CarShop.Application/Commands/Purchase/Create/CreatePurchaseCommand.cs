using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Host.Commands.Purchase
{
    public class CreatePurchaseCommand
    {
        public PurchaseDto Purchase { get; set; }
    }

    public class CreatePurchaseResult
    {
        public CreatePurchaseResult(Guid createdPurchaseId)
        {
            PurchaseId = createdPurchaseId;
        }
        public Guid PurchaseId { get; set; }

    }
}