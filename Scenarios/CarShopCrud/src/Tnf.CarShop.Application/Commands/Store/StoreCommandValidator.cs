using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandValidator : TnfFluentValidator<StoreCommandCreate>
{
    public override void Configure()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(StoreCommandCreate.Name))
            .Length(2, 150)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(StoreCommandCreate.Name));

        RuleFor(command => command.Cnpj)
          .NotEmpty()
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(StoreCommandCreate.Cnpj))
          .Length(2, 18)
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyValid, nameof(StoreCommandCreate.Cnpj));

        RuleFor(command => command.Location)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(StoreCommandCreate.Location))
            .Length(5, 250)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(StoreCommandCreate.Location));
    }
}
