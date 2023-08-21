using Tnf.CarShop.Application.Commands.Store.Update;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Update;

public class UpdateStoreCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_Name_Is_Null()
    {
        var validator = new UpdateStoreCommandValidator();
        var command = new UpdateStoreCommand(Guid.NewGuid(), null, "Test", "cnpj");


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Name) && e.ErrorMessage == "Dealer Name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Less_Than_2_Characters()
    {
        var validator = new UpdateStoreCommandValidator();
        var command = new UpdateStoreCommand(Guid.NewGuid(), "T", "Test", "cnpj");


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Name) &&
                 e.ErrorMessage == "Dealer Name should be between 2 and 150 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Location_Is_Null()
    {
        var validator = new UpdateStoreCommandValidator();
        var command = new UpdateStoreCommand(Guid.NewGuid(), "Test", null, "cnpj");


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Location) && e.ErrorMessage == "Location is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Location_Is_Less_Than_5_Characters()
    {
        var validator = new UpdateStoreCommandValidator();
        var command = new UpdateStoreCommand(Guid.NewGuid(), "Test", "Test", "cnpj");


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == nameof(command.Location) &&
                 e.ErrorMessage == "Location should be between 5 and 250 characters long.");
    }
}