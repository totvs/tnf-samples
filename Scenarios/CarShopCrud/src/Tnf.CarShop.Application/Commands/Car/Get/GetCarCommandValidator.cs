using FluentValidation;
using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Commands.Car.Get;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Host.Commands.Car.Get;

public class GetCarCommandValidator : TnfFluentValidator<GetCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CarId)
            .Must(carId => carId != Guid.Empty)
            .When(command => command.CarId.HasValue)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(GetCarCommand.CarId));
    }
}
