﻿using Tnf.CarShop.Application.Commands.Purchase.Get;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Get;

public class GetPurchaseCommandValidatorTests: TesteComom
{
    [Fact]
    public void GetPurchaseCommandValidator_Should_Return_Error_When_PurchaseId_Is_Empty()
    {
        var command = new GetPurchaseCommand { PurchaseId = Guid.Empty };
        var validator = new GetPurchaseCommandValidator();


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        ValidateGenericMessage(result, "The specified condition was not met for 'Purchase Id'.");
    }
}
