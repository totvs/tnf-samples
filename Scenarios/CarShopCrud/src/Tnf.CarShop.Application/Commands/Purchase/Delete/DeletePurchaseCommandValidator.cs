using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Purchase.Delete;

public class DeletePurchaseCommandValidator : TnfFluentValidator<DeletePurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.PurchaseId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(DeletePurchaseCommand.PurchaseId));
    }
}
