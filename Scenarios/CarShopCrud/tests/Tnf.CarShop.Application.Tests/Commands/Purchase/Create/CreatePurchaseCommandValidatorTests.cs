using Tnf.CarShop.Application.Commands.Purchase.Create;
using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Create;

public class CreatePurchaseCommandValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_Purchase_Id_Is_Empty()
    {
        var command = new CreatePurchaseCommand
        {
            Purchase = new PurchaseDto
            (
                Guid.Empty,
                DateTime.Now,
                new CustomerDto
                {
                    Id = Guid.NewGuid()
                },
                new CarDto
                {
                    Id = Guid.NewGuid()
                }
            )
        };


        var validator = new CreatePurchaseCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Purchase Id is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Purchase_Date_Is_In_The_Future()
    {
        var command = new CreatePurchaseCommand
        {
            Purchase = new PurchaseDto
            (
                Guid.NewGuid(),
                DateTime.Now.AddDays(1),
                new CustomerDto
                {
                    Id = Guid.NewGuid()
                },
                new CarDto
                {
                    Id = Guid.NewGuid()
                }
            )
        };


        var validator = new CreatePurchaseCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Purchase Date should be in the past or today.");
    }

    [Fact]
    public void Should_Have_Error_When_Car_Id_Is_Empty()
    {
        var command = new CreatePurchaseCommand
        {
            Purchase = new PurchaseDto
            (
                Guid.NewGuid(),
                DateTime.Now,
                new CustomerDto
                {
                    Id = Guid.NewGuid()
                },
                new CarDto
                {
                    Id = Guid.Empty
                }
            )
        };


        var validator = new CreatePurchaseCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Car Id is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Customer_Id_Is_Empty()
    {
        var command = new CreatePurchaseCommand
        {
            Purchase = new PurchaseDto
            {
                Id = Guid.NewGuid(),
                PurchaseDate = DateTime.Now,
                Customer = new CustomerDto
                {
                    Id = Guid.Empty
                },
                Car = new CarDto
                {
                    Id = Guid.NewGuid()
                }
            }
        };


        var validator = new CreatePurchaseCommandValidator();
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Customer Id is required.");
    }
}