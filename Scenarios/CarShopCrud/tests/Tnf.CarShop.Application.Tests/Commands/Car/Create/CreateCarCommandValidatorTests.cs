using Tnf.CarShop.Application.Commands.Car;

namespace Tnf.CarShop.Tests.Commands.Car.Create;

public class CarCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_Brand_Is_Null()
    {
        var command = new CarCommand
        {
            Brand = null,
            Model = "Model",
            Year = 2022,
            Price = 79999,
            StoreId = Guid.NewGuid()
        };

        var validator = new CarCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) && e.ErrorMessage == "'Brand' must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Empty()
    {
        var command = new CarCommand
        {
            Brand = string.Empty,
            Model = "Model",
            Year = 2000,
            Price = 10000,
            StoreId = Guid.NewGuid()
        };

        var validator = new CarCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) && e.ErrorMessage == "'Brand' must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Too_Short()
    {
        var command = new CarCommand
        {
            Brand = "A",
            Model = "Model",
            Year = 2000,
            Price = 10000,
            StoreId = Guid.NewGuid()
        };

        var validator = new CarCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) &&
                 e.ErrorMessage == $"'Brand' must be between 2 and 100 characters. You entered {command.Brand.Length} characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Too_Long()
    {
        var command = new CarCommand
        {
            Brand = new string('A', 101),
            Model = "Model",
            Year = 2000,
            Price = 10000,
            StoreId = Guid.NewGuid()
        };

        var validator = new CarCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Brand) &&
                 e.ErrorMessage == $"'Brand' must be between 2 and 100 characters. You entered {command.Brand.Length} characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Null()
    {
        var command = new CarCommand
        {
            Brand = "Brand",
            Model = null,
            Year = 2000,
            Price = 10000,
            StoreId = Guid.NewGuid()
        };

        var validator = new CarCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) && e.ErrorMessage == "'Model' must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Empty()
    {
        var command = new CarCommand
        {
            Brand = "Brand",
            Model = string.Empty,
            Year = 2000,
            Price = 10000,
            StoreId = Guid.NewGuid()
        };

        var validator = new CarCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) && e.ErrorMessage == "'Model' must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Too_Short()
    {
        var command = new CarCommand
        {
            Brand = "Brand",
            Model = "A",
            Year = 2000,
            Price = 10000,
            StoreId = Guid.NewGuid()
        };

        var validator = new CarCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) &&
                 e.ErrorMessage == $"'Model' must be between 2 and 100 characters. You entered {command.Model.Length} characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Model_Is_Too_Long()
    {
        var command = new CarCommand
        {
            Brand = "Brand",
            Model = new string('A', 101),
            Year = 2000,
            Price = 10000,
            StoreId = Guid.NewGuid()
        };

        var validator = new CarCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Model) &&
                 e.ErrorMessage == $"'Model' must be between 2 and 100 characters. You entered {command.Model.Length} characters.");
    }

  
}
