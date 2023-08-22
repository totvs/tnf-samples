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

        RuleFor(command => command.PurchaseDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.BeforeValidDate);

        RuleFor(command => command.Price)
          .NotNull()
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdatePurchaseCommand.Price));

        RuleFor(command => command.CarId)
            .NotNull()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdatePurchaseCommand.CarId));

        RuleFor(command => command.CustomerId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdatePurchaseCommand.CustomerId));

        RuleFor(command => command.StoreId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdatePurchaseCommand.StoreId));
    }
}
