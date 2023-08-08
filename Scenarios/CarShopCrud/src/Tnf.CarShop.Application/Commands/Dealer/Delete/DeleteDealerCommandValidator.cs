using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Dealer.Delete;

public class DeleteDealerCommandValidator : TnfFluentValidator<DeleteDealerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.DealerId)
            .NotEmpty().WithMessage("DealerId is required.");
    }
}