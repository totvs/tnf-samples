using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Car.Delete;

public class DeleteCarCommandValidator : TnfFluentValidator<DeleteCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CarId)
            .NotEmpty().WithMessage("CarId is required.");
    }
}