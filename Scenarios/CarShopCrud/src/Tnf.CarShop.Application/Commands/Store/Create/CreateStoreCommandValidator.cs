using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Store.Create;

public class CreateStoreCommandValidator : TnfFluentValidator<CreateStoreCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateStoreCommand.Name))
            .Length(2, 150)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(CreateStoreCommand.Name));

        RuleFor(command => command.Cnpj)
          .NotEmpty()
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateStoreCommand.Cnpj))
          .Length(2, 18)
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyValid, nameof(CreateStoreCommand.Cnpj));

        RuleFor(command => command.Location)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateStoreCommand.Location))
            .Length(5, 250)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(CreateStoreCommand.Location));
    }
}
