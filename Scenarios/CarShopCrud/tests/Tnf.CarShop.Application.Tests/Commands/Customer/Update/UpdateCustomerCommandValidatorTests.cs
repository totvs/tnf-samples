using Tnf.CarShop.Application.Commands.Customer.Update;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Update;

public class UpdateCustomerCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var command = new UpdateCustomerCommand
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe",
            Address = "Rua Teste",
            Phone = "+55 11 99999-9999",
            Email = "john@doe.com",
            DateOfBirth = DateTime.Today.AddDays(1)
        };


        var validator = new UpdateCustomerCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Date of Birth should be in the past.");
    }

    [Fact]
    public void Should_Have_Error_When_Age_Is_Less_Than_18()
    {
        var command = new UpdateCustomerCommand
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe",
            Address = "Rua Teste",
            Phone = "+55 11 99999-9999",
            Email = "john@doe.com",
            DateOfBirth = DateTime.Today.AddYears(-17)
        };


        var validator = new UpdateCustomerCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Age should be between 18 and 100.");
    }

    [Fact]
    public void Should_Have_Error_When_Age_Is_Greater_Than_100()
    {
        var command = new UpdateCustomerCommand
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe",
            Address = "Rua Teste",
            Phone = "+55 11 99999-9999",
            Email = "john@doe.com",
            DateOfBirth = DateTime.Today.AddYears(-101)
        };


        var validator = new UpdateCustomerCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Age should be between 18 and 100.");
    }
}