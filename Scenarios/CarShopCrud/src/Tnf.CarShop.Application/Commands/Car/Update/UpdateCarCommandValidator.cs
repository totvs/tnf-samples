using FluentValidation;
using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Car.Update;

public class UpdateCarCommandValidator : TnfFluentValidator<UpdateCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateCarCommand.Id));

        RuleFor(command => command.Brand)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateCarCommand.Brand))
            .Length(2, 100)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(UpdateCarCommand.Brand));

        RuleFor(command => command.Model)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateCarCommand.Model))
            .Length(2, 100)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(UpdateCarCommand.Model));

        RuleFor(command => command.Year)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.CarYearLength, DateTime.Now.Year);

        RuleFor(command => command.Price)
            .GreaterThan(0)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PriceShouldBePositive);
    }
}
