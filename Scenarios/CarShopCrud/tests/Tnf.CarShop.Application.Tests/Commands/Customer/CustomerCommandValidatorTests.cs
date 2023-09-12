using Tnf.CarShop.Application.Commands.Customer;

namespace Tnf.CarShop.Application.Tests.Commands.Customer;
public class CustomerCommandValidatorTests : TestBase
{
    [Fact]
    public void Should_Have_Error_When_FullName_Is_Null()
    {
        var command = new CustomerCommandCreate { FullName = null };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, "Full Name");
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Empty()
    {
        var command = new CustomerCommandCreate { FullName = string.Empty };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, "Full Name");
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Too_Short()
    {
        var command = new CustomerCommandCreate { FullName = "A" };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        int size = command.FullName.Length;

        var validationFailure = result.Errors.Where(x => x.PropertyName == nameof(command.FullName)).FirstOrDefault();

        ValidatePropertySize(
            result,
            "Full Name",
            size,
            validationFailure.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            validationFailure.FormattedMessagePlaceholderValues["MaxLength"].ToString());
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Too_Long()
    {
        int size = 151;
        var command = new CustomerCommandCreate { FullName = new string('*', size) };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);

        size = command.FullName.Length;

        var validationFailure = result.Errors.Where(x => x.PropertyName == nameof(command.FullName)).FirstOrDefault();

        ValidatePropertySize(
            result,
            "Full Name",
            size,
            validationFailure.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            validationFailure.FormattedMessagePlaceholderValues["MaxLength"].ToString());
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Null()
    {
        var command = new CustomerCommandCreate { Address = null };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, nameof(command.Address));
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Empty()
    {
        var command = new CustomerCommandCreate { Address = string.Empty };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, nameof(command.Address));
    }


    [Fact]
    public void Should_Have_Error_When_Address_Is_Too_Long()
    {
        var command = new CustomerCommandCreate { Address = new string('*', 251) };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        int size = command.Address.Length;

        var validationFailure = result.Errors.Where(x => x.PropertyName == nameof(command.Address)).FirstOrDefault();

        ValidatePropertySize(
            result,
            nameof(command.Address),
            size,
            validationFailure.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            validationFailure.FormattedMessagePlaceholderValues["MaxLength"].ToString());
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Null()
    {
        var command = new CustomerCommandCreate { Phone = null };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, nameof(command.Phone));
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Empty()
    {
        var command = new CustomerCommandCreate { Phone = string.Empty };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, nameof(command.Phone));
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Invalid()
    {
        var command = new CustomerCommandCreate { Phone = "12345678" };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidatePropertyFormat(result, nameof(command.Phone));
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Null()
    {
        var command = new CustomerCommandCreate { Email = null };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, nameof(command.Email));
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new CustomerCommandCreate { Email = string.Empty };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, nameof(command.Email));
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new CustomerCommandCreate { Email = "invalid" };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateValidEmail(result, nameof(command.Email));
        ValidatePropertyFormat(result, nameof(command.Email));
    }

    [Fact]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var command = new CustomerCommandCreate { DateOfBirth = DateTime.Today.AddDays(1) };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidatePropertyLessThanValue(result, "Date Of Birth", DateTime.Today.ToString());
    }

    [Fact]
    public void Should_Have_Error_When_Age_Is_Less_Than_18()
    {
        var command = new CustomerCommandCreate
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe",
            Address = "Rua Teste",
            Phone = "+55 11 99999-9999",
            Email = "john@doe.com",
            DateOfBirth = DateTime.Today.AddYears(-17)
        };

        var validator = new CustomerCommandValidator();
        var result = validator.Validate(command);

        ValidatePropertyConditionNotMet(result, "Date Of Birth");
    }

    [Fact]
    public void Should_Have_Error_When_Age_Is_Greater_Than_100()
    {
        var command = new CustomerCommandCreate
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe",
            Address = "Rua Teste",
            Phone = "+55 11 99999-9999",
            Email = "john@doe.com",
            DateOfBirth = DateTime.Today.AddYears(-101)
        };

        var validator = new CustomerCommandValidator();
        var result = validator.Validate(command);

        ValidatePropertyConditionNotMet(result, "Date Of Birth");
    }
}
