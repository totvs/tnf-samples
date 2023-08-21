using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Purchase.Create;

public class CreatePurchaseCommandValidator : TnfFluentValidator<CreatePurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Purchase Id is required.");

        RuleFor(command => command.PurchaseDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Purchase Date should be in the past or today.");

        RuleFor(command => command.CarId)
            .NotEmpty().WithMessage("Car Id is required.");
        RuleFor(command => command.StoreId)
            .NotEmpty().WithMessage("Store Id is required.");

        RuleFor(command => command.CustomerId)
            .NotEmpty().WithMessage("Customer Id is required.");

        RuleFor(command => command.TenantId)
           .NotEmpty().WithMessage("TenantId is required.");

        RuleFor(command => command.Price)
           .NotEmpty().WithMessage("Price is required.");
    }
}
