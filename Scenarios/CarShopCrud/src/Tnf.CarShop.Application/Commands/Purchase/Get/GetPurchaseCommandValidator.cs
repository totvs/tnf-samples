using FluentValidation;
using Tnf.CarShop.Application.Commands.Purchase.Delete;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Purchase.Get;

public class GetPurchaseCommandValidator : TnfFluentValidator<GetPurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.PurchaseId)
            .Must(carId => carId != Guid.Empty)
            .When(command => command.PurchaseId.HasValue)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.EmptyGUID, nameof(GetPurchaseCommand.PurchaseId));
    }
}
