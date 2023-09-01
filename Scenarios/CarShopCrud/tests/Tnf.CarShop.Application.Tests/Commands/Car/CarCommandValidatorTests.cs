using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Car;

namespace Tnf.CarShop.Application.Tests.Commands.Car;
public class CarCommandValidatorTests : TestBase
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

        ValidateNullOrEmpty(result, nameof(command.Brand));
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

        ValidateNullOrEmpty(result, nameof(command.Brand));
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
        
        int size = command.Brand.Length;

        result.Errors.ForEach(x => ValidatePropertySize(
            result,
            nameof(command.Brand),
            size,
            x.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            x.FormattedMessagePlaceholderValues["MaxLength"].ToString()));
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

        int size = command.Brand.Length;

        result.Errors.ForEach(x => ValidatePropertySize(
            result,
            nameof(command.Brand),
            size,
            x.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            x.FormattedMessagePlaceholderValues["MaxLength"].ToString()));
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

        ValidateNullOrEmpty(result, nameof(command.Model));
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

        ValidateNullOrEmpty(result, nameof(command.Model));
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

        int size = command.Model.Length;

        result.Errors.ForEach(x => ValidatePropertySize(
            result,
            nameof(command.Model),
            size,
            x.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            x.FormattedMessagePlaceholderValues["MaxLength"].ToString()));
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

        int size = command.Model.Length;

        result.Errors.ForEach(x => ValidatePropertySize(
            result,
            nameof(command.Model),
            size,
            x.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            x.FormattedMessagePlaceholderValues["MaxLength"].ToString()));
    }

    [Fact]
    public void Should_Have_Error_When_Price_Is_Less_Than_0()
    {
        var command = new CarCommand { Price = -1 };
        var validator = new CarCommandValidator();

        var result = validator.Validate(command);

        var validationFailure = result.Errors.Where(x => x.PropertyName == nameof(command.Price)).FirstOrDefault();

        string value = validationFailure?.FormattedMessagePlaceholderValues["ComparisonValue"].ToString();
        ValidatePropertyValue(result, nameof(command.Price), value);
    }
}
