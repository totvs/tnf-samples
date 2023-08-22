using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Customer.Update;

public class UpdateCustomerCommandValidator : TnfFluentValidator<UpdateCustomerCommand>
{
    private bool BeAValidAge(DateTime dateOfBirth)
    {
        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth > DateTime.Today.AddYears(-age))
            age--;

        return age is >= 18 and <= 100;
    }

    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateCustomerCommand.Id));

        RuleFor(command => command.FullName)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateCustomerCommand.FullName))
            .Length(2, 150)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(UpdateCustomerCommand.FullName));

        RuleFor(command => command.Address)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateCustomerCommand.Address))
            .Length(5, 250)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.AddressLength);

        RuleFor(command => command.Phone)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateCustomerCommand.Phone))
            .Matches(@"^(\+55)?\s?\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.ValidBrazilianPhoneNumber);

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(UpdateCustomerCommand.Email))
            .EmailAddress().WithMessage("A valid email is required.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyValid, nameof(UpdateCustomerCommand.Email));

        RuleFor(command => command.DateOfBirth)
            .LessThan(DateTime.Today)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.BeforeValidDate)
            .Must(BeAValidAge)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.Over18YearsOld);
    }
}
