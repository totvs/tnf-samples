using Tnf.CarShop.Application.Commands.Car;
using Tnf.CarShop.Application.Tests.Commands;

namespace Tnf.CarShop.Tests.Validators;

public class CarCommandValidatorTests : TestCommon
{ 

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Empty()
    {
        var command = new CarCommand { Brand = string.Empty };
        var validator = new CarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);

        ValidateEmpty(result, "Brand");
    }


    [Fact]
    public void Should_Have_Error_When_Model_Is_Empty()
    {
        var command = new CarCommand { Model = string.Empty };
        var validator = new CarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);

        ValidateEmpty(result, "Model");

    }


    [Fact]
    public void Should_Have_Error_When_Price_Is_Less_Than_0()
    {
        var command = new CarCommand { Price = -1 };
        var validator = new CarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        ValidateGenericMessage(result, "'Price' must be greater than '0'.");
    }
}
