using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Purchase.Delete;

public class DeletePurchaseCommandValidator : TnfFluentValidator<DeletePurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.PurchaseId)
            .NotEmpty().WithMessage("PurchaseId is required.");
    }
}