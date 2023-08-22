using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Car.Delete;

public class DeleteCarCommandValidator : TnfFluentValidator<DeleteCarCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CarId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(DeleteCarCommand.CarId));
    }
}
