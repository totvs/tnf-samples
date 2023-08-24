using Tnf.CarShop.Application.Commands.Store.Get;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Get;

public class GetStoreCommandValidatorTests: TesteComom
{
    [Fact]
    public void Should_Have_Error_When_StoreId_Is_Null()
    {
        var validator = new GetStoreCommandValidator();
        var command = new GetStoreCommand { StoreId = null };


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        ValidateEmpty(result, "Store Id");
    }
}
