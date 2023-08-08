using FluentValidation;
using Tnf.CarShop.Application.Commands.Car.Create;

namespace Tnf.CarShop.Host.Commands.Car.Create;

public class CreateCarCommandValidator : TnfFluentValidator<CreateCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Car.Brand)
            .NotEmpty().WithMessage("Brand is required.")
            .Length(2, 100).WithMessage("Brand should be between 2 and 100 characters long.");

        RuleFor(command => command.Car.Model)
            .NotEmpty().WithMessage("Model is required.")
            .Length(2, 100).WithMessage("Model should be between 2 and 100 characters long.");

        RuleFor(command => command.Car.Year)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithMessage($"Year should be between 1900 and {DateTime.Now.Year}.");

        RuleFor(command => command.Car.Price)
            .GreaterThan(0).WithMessage("Price should be positive.");
        
        RuleFor(command => command.Car.Dealer)
            .NotNull().WithMessage("Dealer is required.");
        
        RuleFor(command => command.Car.Dealer!.Id)
            .NotNull().WithMessage("DealerId is required.");
    }
}