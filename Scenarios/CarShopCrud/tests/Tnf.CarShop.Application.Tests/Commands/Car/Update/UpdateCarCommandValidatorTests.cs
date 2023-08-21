using Tnf.CarShop.Application.Commands.Car.Update;

namespace Tnf.CarShop.Tests.Validators;

public class UpdateCarCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new UpdateCarCommand { Id = Guid.Empty };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Id) && e.ErrorMessage == "CarId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Empty()
    {
        var command = new UpdateCarCommand { Brand = string.Empty };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) && e.ErrorMessage == "Brand is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Brand_Length_Is_Less_Than_2()
    {
        var command = new UpdateCarCommand { Brand = "A" };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) &&
                 e.ErrorMessage == "Brand should be between 2 and 100 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Empty()
    {
        var command = new UpdateCarCommand { Model = string.Empty };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) && e.ErrorMessage == "Model is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Length_Is_Less_Than_2()
    {
        var command = new UpdateCarCommand { Model = "A" };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) &&
                 e.ErrorMessage == "Model should be between 2 and 100 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Year_Is_Less_Than_1900()
    {
        var command = new UpdateCarCommand { Year = 1899 };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Year) &&
                 e.ErrorMessage == $"Year should be between 1900 and {DateTime.Now.Year}.");
    }

    [Fact]
    public void Should_Have_Error_When_Price_Is_Less_Than_0()
    {
        var command = new UpdateCarCommand { Price = -1 };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Price) && e.ErrorMessage == "Price should be positive.");
    }
}