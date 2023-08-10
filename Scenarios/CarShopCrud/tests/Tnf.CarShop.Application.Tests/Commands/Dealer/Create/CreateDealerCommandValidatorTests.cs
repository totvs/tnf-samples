using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Dealer.Create;
using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Tests.Commands.Dealer.Create
{
    public class CreateDealerCommandValidatorTests
    {
        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            
            var command = new CreateDealerCommand
            {
                Dealer = new DealerDto
                {
                    Name = string.Empty,
                    Location = "Location"
                }
            };

            
            var validator = new CreateDealerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Dealer Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Long()
        {
            
            var command = new CreateDealerCommand
            {
                Dealer = new DealerDto
                {
                    Name = new string('*', 151),
                    Location = "Location"
                }
            };

            
            var validator = new CreateDealerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Dealer Name should be between 2 and 150 characters long.");
        }

        [Fact]
        public void Should_Have_Error_When_Location_Is_Empty()
        {
            
            var command = new CreateDealerCommand
            {
                Dealer = new DealerDto
                {
                    Name = "Name",
                    Location = string.Empty
                }
            };

            
            var validator = new CreateDealerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Location is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Location_Is_Too_Long()
        {
            
            var command = new CreateDealerCommand
            {
                Dealer = new DealerDto
                {
                    Name = "Name",
                    Location = new string('*', 251)
                }
            };

            
            var validator = new CreateDealerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Location should be between 5 and 250 characters long.");
        }

        [Fact]
        public void Should_Be_Valid_When_Name_And_Location_Are_Valid()
        {
            
            var command = new CreateDealerCommand
            {
                Dealer = new DealerDto
                {
                    Name = "Name",
                    Location = "Location"
                }
            };

            
            var validator = new CreateDealerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.True(result.IsValid);
        }
    }
}
