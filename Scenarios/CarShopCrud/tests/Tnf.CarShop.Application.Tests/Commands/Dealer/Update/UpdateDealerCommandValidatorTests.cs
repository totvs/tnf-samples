using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Dealer.Update;
using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Tests.Commands.Dealer.Update
{
    public class UpdateDealerCommandValidatorTests
    {
        private readonly UpdateDealerCommandValidator _validator = new UpdateDealerCommandValidator();

        [Fact]
        public void Should_Have_Error_When_DealerId_Is_Empty()
        {
            var command = new UpdateDealerCommand
            {
                Dealer = new DealerDto { Id = Guid.Empty } 
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Dealer.Id)
                  .WithErrorMessage("Dealer Id is required.");
        }

        [Fact]
        public void Should_Have_Error_When_DealerName_Is_Empty()
        {
            var command = new UpdateDealerCommand
            {
                Dealer = new DealerDto { Name = string.Empty }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Dealer.Name)
                  .WithErrorMessage("Dealer Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_DealerName_Is_Too_Short()
        {
            var command = new UpdateDealerCommand
            {
                Dealer = new DealerDto { Name = "A" }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Dealer.Name)
                  .WithErrorMessage("Dealer Name should be between 2 and 150 characters long.");
        }

        [Fact]
        public void Should_Have_Error_When_DealerName_Is_Too_Long()
        {
            var longName = new string('A', 151);
            var command = new UpdateDealerCommand
            {
                Dealer = new DealerDto { Name = longName }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Dealer.Name)
                  .WithErrorMessage("Dealer Name should be between 2 and 150 characters long.");
        }

        [Fact]
        public void Should_Have_Error_When_Location_Is_Empty()
        {
            var command = new UpdateDealerCommand
            {
                Dealer = new DealerDto { Location = string.Empty }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Dealer.Location)
                  .WithErrorMessage("Location is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Location_Is_Too_Short()
        {
            var command = new UpdateDealerCommand
            {
                Dealer = new DealerDto { Location = "Loc" }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Dealer.Location)
                  .WithErrorMessage("Location should be between 5 and 250 characters long.");
        }

        [Fact]
        public void Should_Have_Error_When_Location_Is_Too_Long()
        {
            var longLocation = new string('L', 251);
            var command = new UpdateDealerCommand
            {
                Dealer = new DealerDto { Location = longLocation }
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Dealer.Location)
                  .WithErrorMessage("Location should be between 5 and 250 characters long.");
        }
    }
}
