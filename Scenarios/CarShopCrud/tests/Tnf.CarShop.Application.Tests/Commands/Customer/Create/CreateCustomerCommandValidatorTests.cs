using FluentAssertions;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Host.Commands.Customer.Create;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Create;

public class CreateCustomerCommandValidatorTests: TestCommon
{
    [Fact]
    public void Should_Have_Error_When_FullName_Is_Null()
    {
        var command = new CreateCustomerCommand { FullName = null };
        var validator = new CreateCustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateEmpty(result, "Full Name");

    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Empty()
    {
        var command = new CreateCustomerCommand { FullName = string.Empty };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        ValidateEmpty(result, "Full Name");
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Too_Short()
    {
        var command = new CreateCustomerCommand { FullName = "A" };
        var validator = new CreateCustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateSizeFullName(result, 1);

    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Too_Long()
    {
        int size = 151;
        var command = new CreateCustomerCommand { FullName = new string('*', size) };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateSizeFullName(result, size);
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Null()
    {
        var command = new CreateCustomerCommand { Address = null };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateEmpty(result, "Address");
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Empty()
    {
        var command = new CreateCustomerCommand { Address = string.Empty };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateEmpty(result, "Address");
    }

 
    [Fact]
    public void Should_Have_Error_When_Address_Is_Too_Long()
    {
        var command = new CreateCustomerCommand { Address = new string('*', 251) };
        var validator = new CreateCustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateAddressTooLong(result);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Null()
    {
        var command = new CreateCustomerCommand { Phone = null };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateEmpty(result, "Phone");
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Empty()
    {
        var command = new CreateCustomerCommand { Phone = string.Empty };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateEmpty(result, "Phone");
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Invalid()
    {
        var command = new CreateCustomerCommand { Phone = "12345678" };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "'Phone' is not in the correct format.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Null()
    {
        var command = new CreateCustomerCommand { Email = null };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateEmpty(result, "Email");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new CreateCustomerCommand { Email = string.Empty };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        ValidateEmpty(result, "Email");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new CreateCustomerCommand { Email = "invalid" };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateValidEmail(result);

    }

    [Fact]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var command = new CreateCustomerCommand { DateOfBirth = DateTime.Today.AddDays(1) };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "'Date Of Birth' must be less than '" + DateTime.Today + "'.");
    }
}
