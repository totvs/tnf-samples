using FluentValidation;
using Tnf.CarShop.Application.Commands.Store.Create;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Store.Delete;

public class DeleteStoreCommandValidator : TnfFluentValidator<DeleteStoreCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.StoreId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(DeleteStoreCommand.StoreId));
    }
}
