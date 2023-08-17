using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Car.Update;
//use xunit
public class UpdateCarCommandValidator : TnfFluentValidator<UpdateCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("CarId is required.");

        RuleFor(command => command.Brand)
            .NotEmpty().WithMessage("Brand is required.")
            .Length(2, 100).WithMessage("Brand should be between 2 and 100 characters long.");

        RuleFor(command => command.Model)
            .NotEmpty().WithMessage("Model is required.")
            .Length(2, 100).WithMessage("Model should be between 2 and 100 characters long.");

        RuleFor(command => command.Year)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithMessage($"Year should be between 1900 and {DateTime.Now.Year}.");

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Price should be positive.");
    }
}
//Unit Test
