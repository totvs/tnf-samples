using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Purchase;
public class PurchaseCommandValidator : TnfFluentValidator<PurchaseCommandCreate>
{
    public override void Configure()
    {
        RuleFor(command => command.PurchaseDate)
            .InclusiveBetween(new DateTime(1900, 01, 01), DateTime.UtcNow)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.BeforeValidDate);

        RuleFor(command => command.CarId)
            .Must(carId => carId != Guid.Empty)
            .When(command => command.CarId == default)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(PurchaseCommandCreate.CarId));

        RuleFor(command => command.StoreId)
            .Must(storeId => storeId != Guid.Empty)
            .When(command => command.StoreId == default)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(PurchaseCommandCreate.StoreId));

        RuleFor(command => command.CustomerId)
            .Must(customerId => customerId != Guid.Empty)
            .When(command => command.CustomerId == default)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(PurchaseCommandCreate.CustomerId));

        RuleFor(command => command.Price)
           .GreaterThan(0)
           .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PriceShouldBePositive);
    }
}
