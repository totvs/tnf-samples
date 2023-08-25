using Tnf.CarShop.Application.Commands.Car.Get;
using Tnf.CarShop.Host.Commands.Car.Get;

namespace Tnf.CarShop.Application.Tests.Commands.Car.Get;

public class GetCarCommandValidatorTests: TestCommon
{
    [Fact]
    public void Should_Have_Error_When_CarId_Is_Empty()
    {
        var validator = new GetCarCommandValidator();
        var command = new GetCarCommand { CarId = Guid.Empty };

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateGenericMessage(result, "The specified condition was not met for 'Car Id'.");
    }
}
