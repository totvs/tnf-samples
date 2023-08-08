using FluentValidation;
using Tnf.CarShop.Application.Commands.Car.Get;

namespace Tnf.CarShop.Host.Commands.Car.Get;

public class GetCarCommandValidator : AbstractValidator<GetCarCommand>
{
    public GetCarCommandValidator()
    {
        RuleFor(command => command.CarId)
            .Must(carId => carId != Guid.Empty)
            .When(command => command.CarId.HasValue)
            .WithMessage("CarId should not be an empty GUID.");
    }
}