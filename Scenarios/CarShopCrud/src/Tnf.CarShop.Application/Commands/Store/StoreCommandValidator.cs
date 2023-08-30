using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandValidator : TnfFluentValidator<StoreCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(StoreCommand.Name))
            .Length(2, 150)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(StoreCommand.Name));

        RuleFor(command => command.Cnpj)
          .NotEmpty()
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(StoreCommand.Cnpj))
          .Length(2, 18)
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyValid, nameof(StoreCommand.Cnpj));

        RuleFor(command => command.Location)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(StoreCommand.Location))
            .Length(5, 250)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(StoreCommand.Location));
    }
}
