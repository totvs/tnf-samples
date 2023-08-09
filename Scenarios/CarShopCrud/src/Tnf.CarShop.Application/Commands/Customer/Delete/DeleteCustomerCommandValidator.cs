using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Customer.Delete;

public class DeleteCustomerCommandValidator : TnfFluentValidator<DeleteCustomerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");
    }
}