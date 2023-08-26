using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommandValidator : TnfFluentValidator<GetCustomerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CustomerId)
            .Must(customerId => customerId.HasValue && customerId.Value != Guid.Empty)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.EmptyGUID);
    }
}
