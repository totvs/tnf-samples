﻿using Tnf.CarShop.Application.Commands.Store.Update;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Update;

public class UpdateStoreCommandValidatorTests: TestCommon
{
    [Fact]
    public void Should_Have_Error_When_Name_Is_Null()
    {
        var validator = new UpdateStoreCommandValidator();
        var command = new UpdateStoreCommand
        {
            Id = Guid.NewGuid(),
            Name = null,
            Location = "Test",
            Cnpj = "cnpj"
        };

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateEmpty(result, "Name");
    }

    //[Fact]
    //public void Should_Have_Error_When_Name_Is_Less_Than_2_Characters()
    //{
    //    var validator = new UpdateStoreCommandValidator();
    //    var command = new UpdateStoreCommand(Guid.NewGuid(), "T", "Test", "cnpj");


    //    var result = validator.Validate(command);


    //    Assert.False(result.IsValid);
   
    //    ValidateGenericMessage(result, "'Name' must be between 2 and 150 characters.You entered 1 characters.");
     
    //}

    [Fact]
    public void Should_Have_Error_When_Location_Is_Null()
    {
        var validator = new UpdateStoreCommandValidator();
        var command = new UpdateStoreCommand
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Location = null,
            Cnpj = "cnpj"
        };

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        ValidateEmpty(result, "Location");
    }
     
}
