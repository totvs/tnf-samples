using Tnf.CarShop.Application.Commands.Dealer.Delete;

namespace Tnf.CarShop.Application.Tests.Commands.Dealer.Delete;

public class DeleteDealerCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_DealerId_Is_Empty()
    {
        var command = new DeleteDealerCommand();
        var validator = new DeleteDealerCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains("DealerId is required.", result.Errors.Select(e => e.ErrorMessage));
    }
}