using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Dealer.Get;

public class GetStoreCommandValidator : TnfFluentValidator<GetStoreCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.StoreId)
            .Must(carId => carId != Guid.Empty)
            .When(command => command.StoreId.HasValue)
            .WithMessage("StoreId should not be an empty GUID.");
    }
}