using Tnf.CarShop.Application.Commands.Purchase.Update;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Update;

public class UpdatePurchaseCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new UpdatePurchaseCommand { Id = Guid.Empty };
        var validator = new UpdatePurchaseCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Purchase Id is required.");
    }

    [Fact]
    public void Should_Have_Error_When_PurchaseDate_Is_In_Future()
    {
        var command = new UpdatePurchaseCommand { PurchaseDate = DateTime.Now.AddDays(1) };
        var validator = new UpdatePurchaseCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Purchase Date should be in the past or today.");
    }

    [Fact]
    public void Should_Have_Error_When_CustomerId_Is_Empty()
    {
        var command = new UpdatePurchaseCommand { CustomerId = Guid.Empty };
        var validator = new UpdatePurchaseCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Customer Id is required.");
    }
}