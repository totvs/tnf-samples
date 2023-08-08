using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Car.Delete;

public class DeleteCarCommandValidator : TnfFluentValidator<DeleteCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CardId)
            .NotEmpty().WithMessage("CarId is required.");
    }
}