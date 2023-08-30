using Tnf.CarShop.Application.Commands.Customer.Get;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Get;

public class GetCustomerCommandValidatorTests: TestCommon
{
    [Fact]
    public void GetCustomerCommandValidator_Should_Return_Error_When_CustomerId_Is_Empty()
    {
        var command = new GetCustomerCommand { CustomerId = Guid.Empty };
        var validator = new GetCustomerCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);

        ValidateGenericMessage(result, "The specified condition was not met for 'Customer Id'.");
    }
}
