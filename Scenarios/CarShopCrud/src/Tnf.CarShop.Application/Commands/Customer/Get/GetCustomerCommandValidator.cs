using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommandValidator : TnfFluentValidator<GetCustomerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CustomerId)
            .Must(carId => carId != Guid.Empty)
            .When(command => command.CustomerId.HasValue)
            .WithMessage("CustomerId should not be an empty GUID.");
    }
}