using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Dealer.Get;

public class GetDealerCommandValidator : TnfFluentValidator<GetDealerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.DealerId)
            .Must(carId => carId != Guid.Empty)
            .When(command => command.DealerId.HasValue)
            .WithMessage("DealerId should not be an empty GUID.");
    }
}