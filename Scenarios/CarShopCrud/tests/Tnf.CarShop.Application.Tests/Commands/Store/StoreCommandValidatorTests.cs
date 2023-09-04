using Tnf.CarShop.Application.Commands.Store;

namespace Tnf.CarShop.Application.Tests.Commands.Store;

public class StoreCommandValidatorTests : TestBase
{
    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new StoreCommand
        {
            Name = string.Empty,
            Cnpj = "cnpj",
            Location = "Location"
        };

        var validator = new StoreCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateNullOrEmpty(result, nameof(command.Name));
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Less_Than_2_Characters()
    {
        var validator = new StoreCommandValidator();
        var command = new StoreCommand
        {
            Name = "T",
            Cnpj = "cnpj",
            Location = "Location"
        };

        var result = validator.Validate(command);

        var size = command.Name.Length;

        var validationFailure = result.Errors.Where(x => x.PropertyName == nameof(command.Name)).FirstOrDefault();

        ValidatePropertySize(
            result,
            nameof(command.Name),
            size,
            validationFailure.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            validationFailure.FormattedMessagePlaceholderValues["MaxLength"].ToString());
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        var command = new StoreCommand
        {
            Name = new string('*', 151),
            Cnpj = "cnpj",
            Location = "Location"
        };

        var validator = new StoreCommandValidator();

        var result = validator.Validate(command);

        var size = command.Name.Length;

        var validationFailure = result.Errors.Where(x => x.PropertyName == nameof(command.Name)).FirstOrDefault();

        ValidatePropertySize(
            result,
            nameof(command.Name),
            size,
            validationFailure.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            validationFailure.FormattedMessagePlaceholderValues["MaxLength"].ToString());
    }

    [Fact]
    public void Should_Have_Error_When_Location_Is_Empty()
    {
        var command = new StoreCommand
        {
            Name = "Name",
            Cnpj = "cnpj",
            Location = string.Empty
        };

        var validator = new StoreCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateNullOrEmpty(result, nameof(command.Location));
    }

    [Fact]
    public void Should_Have_Error_When_Location_Is_Too_Long()
    {
        var command = new StoreCommand
        {
            Name = "Name",
            Cnpj = "cnpj",
            Location = new string('*', 251)
        };

        var validator = new StoreCommandValidator();

        var result = validator.Validate(command);

        var size = command.Location.Length;

        var validationFailure = result.Errors.Where(x => x.PropertyName == nameof(command.Location)).FirstOrDefault();

        ValidatePropertySize(
            result,
            nameof(command.Location),
            size,
            validationFailure.FormattedMessagePlaceholderValues["MinLength"].ToString(),
            validationFailure.FormattedMessagePlaceholderValues["MaxLength"].ToString());
    }
}
