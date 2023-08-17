using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Host.Commands.Car.Create;

namespace Tnf.CarShop.Tests.Commands.Car.Create;

public class CreateCarCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_Brand_Is_Null()
    {
        var command = new CreateCarCommand
        {
            Brand = null,
            Model = "Model",
            Year = 2000,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) && e.ErrorMessage == "Brand is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Empty()
    {
        var command = new CreateCarCommand
        {
            Brand = string.Empty,
            Model = "Model",
            Year = 2000,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) && e.ErrorMessage == "Brand is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Too_Short()
    {
        var command = new CreateCarCommand
        {
            Brand = "A",
            Model = "Model",
            Year = 2000,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) &&
                 e.ErrorMessage == "Brand should be between 2 and 100 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Too_Long()
    {
        var command = new CreateCarCommand
        {
            Brand = new string('A', 101),
            Model = "Model",
            Year = 2000,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) &&
                 e.ErrorMessage == "Brand should be between 2 and 100 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Null()
    {
        var command = new CreateCarCommand
        {
            Brand = "Brand",
            Model = null,
            Year = 2000,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) && e.ErrorMessage == "Model is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Empty()
    {
        var command = new CreateCarCommand
        {
            Brand = "Brand",
            Model = string.Empty,
            Year = 2000,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) && e.ErrorMessage == "Model is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Too_Short()
    {
        var command = new CreateCarCommand
        {
            Brand = "Brand",
            Model = "A",
            Year = 2000,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) &&
                 e.ErrorMessage == "Model should be between 2 and 100 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Too_Long()
    {
        var command = new CreateCarCommand
        {
            Brand = "Brand",
            Model = new string('A', 101),
            Year = 2000,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) &&
                 e.ErrorMessage == "Model should be between 2 and 100 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Year_Is_Lower_Than_1900()
    {
        var command = new CreateCarCommand
        {
            Brand = "Brand",
            Model = "Model",
            Year = 1899,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Year) &&
                 e.ErrorMessage == $"Year should be between 1900 and {DateTime.Now.Year}.");
    }

    [Fact]
    public void Should_Have_Error_When_Year_Is_Greater_Than_Current_Year()
    {
        var command = new CreateCarCommand
        {
            Brand = "Brand",
            Model = "Model",
            Year = DateTime.Now.Year + 1,
            Price = 10000
        };


        var validator = new CreateCarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Year) &&
                 e.ErrorMessage == $"Year should be between 1900 and {DateTime.Now.Year}.");
    }
}