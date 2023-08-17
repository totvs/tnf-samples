
using Tnf.CarShop.Application.Commands.Store.Create;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Create;

public class CreateStoreCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        
        var command = new CreateStoreCommand { Name = string.Empty, Location = "Location" };
        var validator = new CreateStoreCommandValidator();

        
        var result = validator.Validate(command);

        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateStoreCommand.Name) && e.ErrorMessage == "Dealer Name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        
        var command = new CreateStoreCommand { Name = new string('*', 151), Location = "Location" };
        var validator = new CreateStoreCommandValidator();

        
        var result = validator.Validate(command);

        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateStoreCommand.Name) && e.ErrorMessage == "Dealer Name should be between 2 and 150 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Location_Is_Empty()
    {
        
        var command = new CreateStoreCommand { Name = "Name", Location = string.Empty };
        var validator = new CreateStoreCommandValidator();

        
        var result = validator.Validate(command);

        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateStoreCommand.Location) && e.ErrorMessage == "Location is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Location_Is_Too_Long()
    {
        
        var command = new CreateStoreCommand { Name = "Name", Location = new string('*', 251) };
        var validator = new CreateStoreCommandValidator();

        
        var result = validator.Validate(command);

        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateStoreCommand.Location) && e.ErrorMessage == "Location should be between 5 and 250 characters long.");
    }

}