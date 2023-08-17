using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Store.Update;

//use xunit
public class UpdateStoreCommandValidator : TnfFluentValidator<UpdateStoreCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Dealer Id is required.");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Dealer Name is required.")
            .Length(2, 150).WithMessage("Dealer Name should be between 2 and 150 characters long.");

        RuleFor(command => command.Location)
            .NotEmpty().WithMessage("Location is required.")
            .Length(5, 250).WithMessage("Location should be between 5 and 250 characters long.");
    }
}