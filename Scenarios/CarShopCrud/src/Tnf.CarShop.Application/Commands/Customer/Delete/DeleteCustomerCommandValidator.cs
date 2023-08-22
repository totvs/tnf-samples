using FluentValidation;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Customer.Delete;

public class DeleteCustomerCommandValidator : TnfFluentValidator<DeleteCustomerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CustomerId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(DeleteCustomerCommand.CustomerId));
    }
}
