using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Fipe;
public class ApplyFipeTableCommandValidator : TnfFluentValidator<ApplyFipeTableCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.FipeCode)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(ApplyFipeTableCommand.FipeCode));

        RuleFor(command => command.MonthYearReference)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(ApplyFipeTableCommand.MonthYearReference));

        RuleFor(command => command.Brand)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(ApplyFipeTableCommand.Brand));

        RuleFor(command => command.Brand)
            .Length(2, 100)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(ApplyFipeTableCommand.Brand));

        RuleFor(command => command.Model)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(ApplyFipeTableCommand.Model));

        RuleFor(command => command.Model)
            .Length(2, 100)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(ApplyFipeTableCommand.Model));

        RuleFor(command => command.Year)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.CarYearLength, DateTime.Now.Year);

        RuleFor(command => command.AveragePrice)
            .GreaterThan(0)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PriceShouldBePositive);
    }
}
