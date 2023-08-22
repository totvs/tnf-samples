using FluentValidation;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Host.Commands.Customer.Create;

public class CreateCustomerCommandValidator : TnfFluentValidator<CreateCustomerCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.FullName)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateCustomerCommand.FullName))
            .Length(2, 150)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(CreateCustomerCommand.FullName));

        RuleFor(command => command.Address)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateCustomerCommand.Address))
            .Length(5, 250)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.AddressLength);

        RuleFor(command => command.Phone)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateCustomerCommand.Phone))
            .Matches(@"^(\+55)?\s?\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.ValidBrazilianPhoneNumber);

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreateCustomerCommand.Email))
            .EmailAddress()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyValid, nameof(CreateCustomerCommand.Email))
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyValid, nameof(CreateCustomerCommand.Email));

        RuleFor(command => command.DateOfBirth)
            .LessThan(DateTime.Today)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.BeforeValidDate)
            .Must(BeAValidAge)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.Over18YearsOld);
    }

    private bool BeAValidAge(DateTime dateOfBirth)
    {
        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth > DateTime.Today.AddYears(-age))
            age--;

        return age >= 18 && age <= 100;
    }
}
