using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Store.Update;

public class UpdateStoreCommandValidator : TnfFluentValidator<UpdateStoreCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateStoreCommand.Id));

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateStoreCommand.Name))
            .Length(2, 150)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(UpdateStoreCommand.Name));

        RuleFor(command => command.Cnpj)
          .NotEmpty()
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateStoreCommand.Cnpj))
          .Length(2, 18)
          .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(UpdateStoreCommand.Cnpj));

        RuleFor(command => command.Location)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateStoreCommand.Location))
            .Length(5, 250)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(UpdateStoreCommand.Cnpj));
    }
}
