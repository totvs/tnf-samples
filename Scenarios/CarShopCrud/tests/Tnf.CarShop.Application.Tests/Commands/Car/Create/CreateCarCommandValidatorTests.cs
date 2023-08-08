using Xunit;
using FluentValidation.TestHelper;
using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Host.Commands.Car.Create;
using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Tests.Commands.Car.Create
{
    public class CreateCarCommandValidatorTests
    {
        private readonly CreateCarCommandValidator _validator = new CreateCarCommandValidator();

        [Fact]
        public void Should_Error_When_Brand_Is_Empty()
        {
            var model = new CreateCarCommand { Car = new CarDto { Brand = string.Empty } };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.Car.Brand);
            Assert.Contains("Brand is required.", result.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("This is a test string that is intended to be longer than one hundred characters to see if the validation rule kicks in as expected.")]
        public void Should_Error_When_Brand_Length_Is_Outside_Range(string brand)
        {
            var model = new CreateCarCommand { Car = new CarDto { Brand = brand } };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.Car.Brand);
        }

        [Fact]
        public void Should_Error_When_Model_Is_Empty()
        {
            var model = new CreateCarCommand { Car = new CarDto { Model = string.Empty } };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.Car.Model);
            Assert.Contains("Model is required.", result.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("This is a test string that is intended to be longer than one hundred characters to see if the validation rule kicks in as expected.")]
        public void Should_Error_When_Model_Length_Is_Outside_Range(string modelStr)
        {
            var model = new CreateCarCommand { Car = new CarDto { Model = modelStr } };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.Car.Model);
        }

        [Theory]
        [InlineData(1800)]
        [InlineData(3000)]
        public void Should_Error_When_Year_Is_Outside_Range(int year)
        {
            var model = new CreateCarCommand { Car = new CarDto { Year = year } };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.Car.Year);
        }

        [Fact]
        public void Should_Error_When_Price_Is_Negative_Or_Zero()
        {
            var model = new CreateCarCommand { Car = new CarDto { Price = -10 } };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.Car.Price);
            Assert.Contains("Price should be positive.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_Error_When_Dealer_Is_Null()
        {
            var model = new CreateCarCommand { Car = new CarDto { Dealer = null } };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.Car.Dealer);
            Assert.Contains("Dealer is required.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_Error_When_DealerId_Is_Null()
        {
            var model = new CreateCarCommand { Car = new CarDto { Dealer = new DealerDto() } };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.Car.Dealer.Id);
            Assert.Contains("DealerId is required.", result.Errors[0].ErrorMessage);
        }
    }
}
