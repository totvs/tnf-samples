﻿using FluentAssertions;
using Tnf.CarShop.Application.Commands.Customer;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Create;

public class CustomerCommandValidatorTests : TestBase
{
    [Fact]
    public void Should_Have_Error_When_FullName_Is_Null()
    {
        var command = new CustomerCommand { FullName = null };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, "Full Name");

    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Empty()
    {
        var command = new CustomerCommand { FullName = string.Empty };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);


        ValidateNullOrEmpty(result, "Full Name");
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Too_Short()
    {
        var command = new CustomerCommand { FullName = "A" };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateSizeFullName(result, 1);

    }

    [Fact]
    public void Should_Have_Error_When_FullName_Is_Too_Long()
    {
        int size = 151;
        var command = new CustomerCommand { FullName = new string('*', size) };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateSizeFullName(result, size);
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Null()
    {
        var command = new CustomerCommand { Address = null };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, "Address");
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_Empty()
    {
        var command = new CustomerCommand { Address = string.Empty };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, "Address");
    }


    [Fact]
    public void Should_Have_Error_When_Address_Is_Too_Long()
    {
        var command = new CustomerCommand { Address = new string('*', 251) };
        var validator = new CustomerCommandValidator();

        var result = validator.Validate(command);

        ValidateAddressTooLong(result);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Null()
    {
        var command = new CustomerCommand { Phone = null };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, "Phone");
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Empty()
    {
        var command = new CustomerCommand { Phone = string.Empty };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, "Phone");
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Invalid()
    {
        var command = new CustomerCommand { Phone = "12345678" };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "'Phone' is not in the correct format.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Null()
    {
        var command = new CustomerCommand { Email = null };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateNullOrEmpty(result, "Email");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new CustomerCommand { Email = string.Empty };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);


        ValidateNullOrEmpty(result, "Email");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new CustomerCommand { Email = "invalid" };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);

        ValidateValidEmail(result);

    }

    [Fact]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var command = new CustomerCommand { DateOfBirth = DateTime.Today.AddDays(1) };
        var validator = new CustomerCommandValidator();


        var result = validator.Validate(command);


        result.Errors.Should().Contain(e => e.ErrorMessage == "'Date Of Birth' must be less than '" + DateTime.Today + "'.");
    }
}
