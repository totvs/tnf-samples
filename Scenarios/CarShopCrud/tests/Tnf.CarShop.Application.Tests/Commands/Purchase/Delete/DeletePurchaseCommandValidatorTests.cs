using Tnf.CarShop.Application.Commands.Purchase.Delete;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Delete;

public class DeletePurchaseCommandValidatorTests: TestCommon
{
    [Fact]
    public void Should_Have_Error_When_PurchaseId_Is_Empty()
    {
        var command = new DeletePurchaseCommand();
        var validator = new DeletePurchaseCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        ValidateEmpty(result, "Purchase Id");
    }
}
