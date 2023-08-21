using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Purchase.Update;

public class UpdatePurchaseCommandValidator : TnfFluentValidator<UpdatePurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Purchase Id is required.");

        RuleFor(command => command.PurchaseDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Purchase Date should be in the past or today.");

        RuleFor(command => command.Price)
          .NotNull().WithMessage("Price must be present.");

        RuleFor(command => command.CarId)
            .NotNull().WithMessage("Car must be present.");

        RuleFor(command => command.CustomerId)
            .NotEmpty().WithMessage("Customer Id is required.");

        RuleFor(command => command.StoreId)
            .NotEmpty().WithMessage("Store Id is required.");
    }
}
