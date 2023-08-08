using FluentValidation;
using Tnf.CarShop.Application.Commands.Customer.Create;

namespace Tnf.CarShop.Host.Commands.Customer.Create;

public class CreateCustomerCommandValidator : TnfFluentValidator<CreateCustomerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Customer.FullName)
            .NotEmpty().WithMessage("Full Name is required.")
            .Length(2, 150).WithMessage("Full Name should be between 2 and 150 characters long.");

        RuleFor(command => command.Customer.Address)
            .NotEmpty().WithMessage("Address is required.")
            .Length(5, 250).WithMessage("Address should be between 5 and 250 characters long.");

        RuleFor(command => command.Customer.Phone)
            .NotEmpty().WithMessage("Phone is required.")
            .Matches(@"^(\+55)?\s?\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage("A valid Brazilian phone number is required.");

        RuleFor(command => command.Customer.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Invalid email format.");

        RuleFor(command => command.Customer.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of Birth should be in the past.")
            .Must(BeAValidAge).WithMessage("Age should be between 18 and 100.");
    }

    private bool BeAValidAge(DateOnly dateOfBirth)
    {
        int age = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Today.AddYears(-age))) age--;
        return age >= 18 && age <= 100;
    }
}


