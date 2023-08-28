using FluentValidation;
using Tnf.CarShop.Application.Commands.Store.Delete;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Store.Get;

public class GetStoreCommandValidator : TnfFluentValidator<GetStoreCommand>
{
    public override void Configure()
    {
        RuleFor(x => x.StoreId)
            .Must(storeId => storeId.HasValue && storeId != Guid.Empty)            
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(GetStoreCommand.StoreId));
    }
}
