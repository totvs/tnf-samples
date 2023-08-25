﻿using Tnf.CarShop.Application.Commands.Store.Create;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Create;

public class CreateStoreCommandValidatorTests: TestCommon
{
    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new CreateStoreCommand
        {
            Name = string.Empty,
            Cnpj= "cnpj",
            Location = "Location"
        };

        var validator = new CreateStoreCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateEmpty(result, "Name");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        var command = new CreateStoreCommand
        {
            Name = new string('*', 151),
            Cnpj = "cnpj",
            Location = "Location"
        };

        var validator = new CreateStoreCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateGenericMessage(result, "'Name' must be between 2 and 150 characters. You entered 151 characters.");
       
    }

    [Fact]
    public void Should_Have_Error_When_Location_Is_Empty()
    {
        var command = new CreateStoreCommand
        {
            Name = "Name",
            Cnpj = "cnpj",
            Location = string.Empty
        };

        var validator = new CreateStoreCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateEmpty(result, "Location");
    }

    [Fact]
    public void Should_Have_Error_When_Location_Is_Too_Long()
    {
        var command = new CreateStoreCommand
        {
            Name = "Name",
            Cnpj = "cnpj",
            Location = new string('*', 251)
        };

        var validator = new CreateStoreCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateGenericMessage(result, "'Location' must be between 5 and 250 characters. You entered 251 characters.");
    }
}
