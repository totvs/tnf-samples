using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommandValidator : TnfFluentValidator<GetCustomerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CustomerId)
            .Must(customerId => customerId != Guid.Empty)
            .When(command => !command.CustomerId.HasValue)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.EmptyGUID);
    }
}
