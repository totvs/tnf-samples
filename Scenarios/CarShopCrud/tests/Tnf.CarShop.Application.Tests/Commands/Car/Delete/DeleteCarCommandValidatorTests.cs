using Tnf.CarShop.Application.Commands.Car.Delete;

namespace Tnf.CarShop.Application.Tests.Commands.Car.Delete;

public class DeleteCarCommandValidatorTests
{
    [Fact]
    public void DeleteCarCommandValidator_Validate_CarIdIsNotEmpty_ShouldReturnSuccess()
    {
        var validator = new DeleteCarCommandValidator();
        var command = new DeleteCarCommand(Guid.NewGuid());

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }
}
