using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Car;

public class CarCommandValidator : TnfFluentValidator<CarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Brand)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CarCommand.Brand));

        RuleFor(command => command.Brand)
            .Length(2, 100)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(CarCommand.Brand));

        RuleFor(command => command.Model)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CarCommand.Model));

        RuleFor(command => command.Model)
            .Length(2, 100)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(CarCommand.Model));

        RuleFor(command => command.Year)
            .InclusiveBetween(1900, DateTime.Now.Year+1)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.CarYearLength, DateTime.Now.Year+1);

        RuleFor(command => command.Price)
            .GreaterThan(0)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PriceShouldBePositive);

        RuleFor(command => command.StoreId)
            .Must(storeId => storeId != Guid.Empty)
            .When(command => command.StoreId == default)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CarCommand.StoreId));
    }
}
