using FluentValidation;
using Tnf.CarShop.Application.Commands.Car;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Customer;
public class CustomerCommandValidator : TnfFluentValidator<CustomerCommandCreate>
{
    private const string PhoneRegex = @"^(\+55)?\s?\(?\d{2}\)?\s?\d{4,5}-?\d{4}$";
    private const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public override void Configure()
    {
        RuleFor(command => command.FullName)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CustomerCommandCreate.FullName))
            .Length(2, 150)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyLength, nameof(CustomerCommandCreate.FullName));

        RuleFor(command => command.Address)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CustomerCommandCreate.Address))
            .Length(5, 250)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.AddressLength);

        RuleFor(command => command.Phone)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CustomerCommandCreate.Phone))
            .Matches(PhoneRegex)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.ValidBrazilianPhoneNumber);

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CustomerCommandCreate.Email))
            .EmailAddress()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyValid, nameof(CustomerCommandCreate.Email))
            .Matches(EmailRegex)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyValid, nameof(CustomerCommandCreate.Email));

        RuleFor(command => command.DateOfBirth)
            .LessThan(DateTime.Today)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.BeforeValidDate)
            .Must(BeAValidAge)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.Over18YearsOld);

        RuleFor(command => command.StoreId)
            .Must(storeId => storeId != Guid.Empty)
            .When(command => command.StoreId == default)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CustomerCommandCreate.StoreId));
    }

    private bool BeAValidAge(DateTime dateOfBirth)
    {
        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth > DateTime.Today.AddYears(-age))
            age--;

        return age >= 18 && age <= 100;
    }
}
