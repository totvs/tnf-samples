using Xunit;

using FluentValidation.TestHelper;
using Tnf.CarShop.Application.Commands.Car.Update;
using Tnf.CarShop.Application.Dtos;
using System;

namespace Tnf.CarShop.Tests.Validators
{
    public class UpdateCarCommandValidatorTests
    {
        private readonly UpdateCarCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_CarId_Is_Empty()
        {
            var command = new UpdateCarCommand
            {
                Car = new CarDto(Guid.Empty)
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Car.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Brand_Is_Null_Or_Empty()
        {
            var command = new UpdateCarCommand
            {
                Car = new CarDto(Guid.NewGuid()) { Brand = "" }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Car.Brand);
        }

        [Fact]
        public void Should_Have_Error_When_Model_Is_Null_Or_Empty()
        {
            var command = new UpdateCarCommand
            {
                Car = new CarDto(Guid.NewGuid()) { Model = "" }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Car.Model);
        }

        [Fact]
        public void Should_Have_Error_When_Year_Is_Out_Of_Range()
        {
            var command = new UpdateCarCommand
            {
                Car = new CarDto(Guid.NewGuid()) { Year = DateTime.Now.Year + 1 }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Car.Year);
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Not_Positive()
        {
            var command = new UpdateCarCommand
            {
                Car = new CarDto(Guid.NewGuid()) { Price = -10 }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Car.Price);
        }

        [Fact]
        public void Should_Have_Error_When_DealerId_Is_Null()
        {
            var command = new UpdateCarCommand
            {
                Car = new CarDto(Guid.NewGuid())
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Car.Dealer.Id);
        }

        [Fact]
        public void Should_Have_Error_When_DealerId_Is_Empty()
        {
            var command = new UpdateCarCommand
            {
                Car = new CarDto(Guid.NewGuid())
                {
                    Dealer = new DealerDto(Guid.Empty)
                }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor("Car.Dealer.Id");
        }

        // You can also have tests that check if there's no validation error when everything is set correctly

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new UpdateCarCommand
            {
                Car = new CarDto(Guid.NewGuid())
                {
                    Brand = "Toyota",
                    Model = "Camry",
                    Year = DateTime.Now.Year - 1,
                    Price = 50000,
                    Dealer = new DealerDto(Guid.NewGuid())
                }
            };

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
