using Tnf.CarShop.Application.Commands.Store.Delete;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Delete;

public class DeleteStoreCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_StoreId_Is_Empty()
    {
        
        var command = new DeleteStoreCommand();
        var validator = new DeleteStoreCommandValidator();

        
        var result = validator.Validate(command);

        
        Assert.False(result.IsValid);
        Assert.Contains("StoreId is required.", result.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public void Should_Not_Have_Error_When_StoreId_Is_Not_Empty()
    {
        
        var command = new DeleteStoreCommand { StoreId = Guid.NewGuid() };
        var validator = new DeleteStoreCommandValidator();

        
        var result = validator.Validate(command);

        
        Assert.True(result.IsValid);
    }
}