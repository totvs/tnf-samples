using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Purchase.Update;

public class UpdatePurchaseCommandValidator : TnfFluentValidator<UpdatePurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdatePurchaseCommand.Id));        
    }
}
