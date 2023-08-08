using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Purchase.Update;

public class UpdatePurchaseCommandValidator : TnfFluentValidator<UpdatePurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Purchase.Id)
            .NotEmpty().WithMessage("Purchase Id is required.");

        RuleFor(command => command.Purchase.PurchaseDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Purchase Date should be in the past or today.");

        RuleFor(command => command.Purchase.Car)
            .NotNull().WithMessage("Car must be present.");

        RuleFor(command => command.Purchase.Car.Id)
            .NotEmpty().WithMessage("Car Id is required.");

        RuleFor(command => command.Purchase.Customer)
            .NotNull().WithMessage("Customer must be present.");

        RuleFor(command => command.Purchase.Customer.Id)
            .NotEmpty().WithMessage("Customer Id is required.");
    }
}