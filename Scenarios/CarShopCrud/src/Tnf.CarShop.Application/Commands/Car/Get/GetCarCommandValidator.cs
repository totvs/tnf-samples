using FluentValidation;
using Tnf.CarShop.Application.Commands.Car.Get;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Host.Commands.Car.Get;

public class GetCarCommandValidator : TnfFluentValidator<GetCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CarId)
            .Must(carId => carId.HasValue && carId.Value != Guid.Empty)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(GetCarCommand.CarId));
    }
}
