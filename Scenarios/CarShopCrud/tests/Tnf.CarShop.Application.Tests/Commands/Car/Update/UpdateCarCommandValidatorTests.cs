using Tnf.CarShop.Application.Commands.Car.Update;
using Tnf.CarShop.Application.Tests.Commands;
using Tnf.CarShop.Application.Tests.Commands.Car;

namespace Tnf.CarShop.Tests.Validators;

public class UpdateCarCommandValidatorTests: TesteComom
{
    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new UpdateCarCommand { Id = Guid.Empty };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);

        ValidateEmpty(result, "Id");

    }

    [Fact]
    public void Should_Have_Error_When_Brand_Is_Empty()
    {
        var command = new UpdateCarCommand { Brand = string.Empty };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);

        ValidateEmpty(result, "Brand");
    }
    

    [Fact]
    public void Should_Have_Error_When_Model_Is_Empty()
    {
        var command = new UpdateCarCommand { Model = string.Empty };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);

        ValidateEmpty(result, "Model");
       
    }
       

    [Fact]
    public void Should_Have_Error_When_Price_Is_Less_Than_0()
    {
        var command = new UpdateCarCommand { Price = -1 };
        var validator = new UpdateCarCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        ValidateGenericMessage(result, "'Price' must be greater than '0'.");
    }
}
