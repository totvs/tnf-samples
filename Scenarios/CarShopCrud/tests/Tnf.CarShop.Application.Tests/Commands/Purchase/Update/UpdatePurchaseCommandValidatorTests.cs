using Tnf.CarShop.Application.Commands.Purchase.Update;
using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Update;

public class UpdatePurchaseCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_PurchaseId_Is_Null()
    {
        var validator = new UpdatePurchaseCommandValidator();
        var command = new UpdatePurchaseCommand
        {
            Purchase = new PurchaseDto
            {
                Id = Guid.Empty,
                PurchaseDate = DateTime.Now,
                Car = new CarDto { Id = Guid.NewGuid() },
                Customer = new CustomerDto { Id = Guid.NewGuid() }
            }
        };


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Purchase Id is required.");
    }

    [Fact]
    public void Should_Have_Error_When_PurchaseDate_Is_In_Future()
    {
        var validator = new UpdatePurchaseCommandValidator();
        var command = new UpdatePurchaseCommand
        {
            Purchase = new PurchaseDto
            {
                Id = Guid.NewGuid(),
                PurchaseDate = DateTime.Now.AddDays(1),
                Car = new CarDto { Id = Guid.NewGuid() },
                Customer = new CustomerDto { Id = Guid.NewGuid() }
            }
        };


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Purchase Date should be in the past or today.");
    }

    [Fact]
    public void Should_Have_Error_When_CarId_Is_Null()
    {
        var validator = new UpdatePurchaseCommandValidator();
        var command = new UpdatePurchaseCommand
        {
            Purchase = new PurchaseDto
            {
                Id = Guid.NewGuid(),
                PurchaseDate = DateTime.Now,
                Car = new CarDto { Id = Guid.Empty },
                Customer = new CustomerDto { Id = Guid.NewGuid() }
            }
        };


        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Car Id is required.");
    }
}