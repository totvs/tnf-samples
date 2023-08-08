using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Purchase.Get;

public class GetPurchaseCommandValidator : TnfFluentValidator<GetPurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.PurchaseId)
            .Must(carId => carId != Guid.Empty)
            .When(command => command.PurchaseId.HasValue)
            .WithMessage("PurchaseId should not be an empty GUID.");
    }
}