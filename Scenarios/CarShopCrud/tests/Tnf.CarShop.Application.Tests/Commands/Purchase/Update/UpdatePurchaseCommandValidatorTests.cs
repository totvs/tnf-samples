using Tnf.CarShop.Application.Commands.Purchase.Update;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Update;

public class UpdatePurchaseCommandValidatorTests: TestCommon
{
    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new UpdatePurchaseCommand { Id = Guid.Empty };
        var validator = new UpdatePurchaseCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateEmpty(result, "Id");
    }
}
