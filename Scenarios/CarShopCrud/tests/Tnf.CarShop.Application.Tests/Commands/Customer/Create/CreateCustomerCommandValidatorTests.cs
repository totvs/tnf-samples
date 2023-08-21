using FluentAssertions;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Host.Commands.Customer.Create;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Create;

public class CreateCustomerCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_FullName_Is_Null()
    {
        var command = new CreateCustomerCommand { FullName = null };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Full Name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Empty()
    {
        var command = new CreateCustomerCommand { FullName = string.Empty };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Full Name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Too_Short()
    {
        var command = new CreateCustomerCommand { FullName = "A" };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Full Name should be between 2 and 150 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Too_Long()
    {
        var command = new CreateCustomerCommand { FullName = new string('*', 151) };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Full Name should be between 2 and 150 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Null()
    {
        var command = new CreateCustomerCommand { Address = null };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Address is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Empty()
    {
        var command = new CreateCustomerCommand { Address = string.Empty };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Address is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Too_Short()
    {
        var command = new CreateCustomerCommand { Address = "A" };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Address should be between 5 and 250 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Too_Long()
    {
        var command = new CreateCustomerCommand { Address = new string('*', 251) };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Address should be between 5 and 250 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Null()
    {
        var command = new CreateCustomerCommand { Phone = null };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Phone is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Empty()
    {
        var command = new CreateCustomerCommand { Phone = string.Empty };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Phone is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Invalid()
    {
        var command = new CreateCustomerCommand { Phone = "12345678" };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "A valid Brazilian phone number is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Null()
    {
        var command = new CreateCustomerCommand { Email = null };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new CreateCustomerCommand { Email = string.Empty };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new CreateCustomerCommand { Email = "invalid" };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "A valid email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var command = new CreateCustomerCommand { DateOfBirth = DateTime.Today.AddDays(1) };
        var validator = new CreateCustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "Date of Birth should be in the past.");
    }
}