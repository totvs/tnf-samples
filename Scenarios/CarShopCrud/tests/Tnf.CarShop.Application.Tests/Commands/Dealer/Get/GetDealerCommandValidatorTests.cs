using Tnf.CarShop.Application.Commands.Dealer.Get;

namespace Tnf.CarShop.Application.Tests.Commands.Dealer.Get;

public class GetDealerCommandValidatorTests
{
    [Fact]
    public void GetDealerCommandValidator_Should_Return_Error_When_DealerId_Is_Empty()
    {
        var command = new GetDealerCommand { DealerId = Guid.Empty };
        var validator = new GetDealerCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.ErrorMessage == "DealerId should not be an empty GUID.");
    }
}