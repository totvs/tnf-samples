using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Customer.Update;

public class UpdateCustomerCommandValidator : TnfFluentValidator<UpdateCustomerCommand>
{
    private bool BeAValidAge(DateTime dateOfBirth)
    {
        var age = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;
        return age is >= 18 and <= 100;
    }

    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(command => command.FullName)
            .NotEmpty().WithMessage("Full Name is required.")
            .Length(2, 150).WithMessage("Full Name should be between 2 and 150 characters long.");

        RuleFor(command => command.Address)
            .NotEmpty().WithMessage("Address is required.")
            .Length(5, 250).WithMessage("Address should be between 5 and 250 characters long.");

        RuleFor(command => command.Phone)
            .NotEmpty().WithMessage("Phone is required.")
            .Matches(@"^(\+55)?\s?\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage("A valid Brazilian phone number is required.");

        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Invalid email format.");

        RuleFor(command => command.DateOfBirth)
            .LessThan(DateTime.Today).WithMessage("Date of Birth should be in the past.")
            .Must(BeAValidAge).WithMessage("Age should be between 18 and 100.");
    }
}
