using FluentValidation;
using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Host.Commands.Car.Create;

public class CreateCarCommandValidator : TnfFluentValidator<CreateCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Brand)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateCarCommand.Brand));

        RuleFor(command => command.Brand)
            .Length(2, 100)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(CreateCarCommand.Brand));

        RuleFor(command => command.Model)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateCarCommand.Model));

        RuleFor(command => command.Model)
            .Length(2, 100)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(CreateCarCommand.Model));

        RuleFor(command => command.Year)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.CarYearLength, DateTime.Now.Year);

        RuleFor(command => command.Price)
            .GreaterThan(0)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PriceShouldBePositive);
    }
}
