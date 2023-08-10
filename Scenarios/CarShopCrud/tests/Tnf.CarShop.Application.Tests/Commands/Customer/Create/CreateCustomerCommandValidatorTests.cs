using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Host.Commands.Customer.Create;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Create
{
    public class CreateCustomerCommandValidatorTests
    {

        [Fact]
        public void Should_have_error_when_full_name_is_empty()
        {
            
            var command = new CreateCustomerCommand
            {
                Customer = new CustomerDto
                {
                    FullName = "",
                    Address = "Rua Teste",
                    Phone = "+55 11 99999-9999",
                    Email = "test@test.com",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
                }
            };

            
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Equal("Full Name is required.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_have_error_when_full_name_is_invalid_length()
        {
            
            var command = new CreateCustomerCommand
            {
                Customer = new CustomerDto
                {
                    FullName = "A",
                    Address = "Rua Teste",
                    Phone = "+55 11 99999-9999",
                    Email = "test@test.com",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
                }
            };

            
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Equal("Full Name should be between 2 and 150 characters long.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_have_error_when_address_is_empty()
        {
            
            var command = new CreateCustomerCommand
            {
                Customer = new CustomerDto
                {
                    FullName = "John Doe",
                    Address = "",
                    Phone = "+55 11 99999-9999",
                    Email = "test@test.com",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
                }
            };

            
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Equal("Address is required.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_have_error_when_address_is_invalid_length()
        {
            
            var command = new CreateCustomerCommand
            {
                Customer = new CustomerDto
                {
                    FullName = "John Doe",
                    Address = "Test",
                    Phone = "+55 11 99999-9999",
                    Email = "test@test.com",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
                }
            };

            
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Equal("Address should be between 5 and 250 characters long.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_have_error_when_phone_is_empty()
        {
            
            var command = new CreateCustomerCommand
            {
                Customer = new CustomerDto
                {
                    FullName = "John Doe",
                    Address = "Rua Teste",
                    Phone = "",
                    Email = "test@test.com",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
                }
            };

            
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Equal("Phone is required.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_have_error_when_email_is_empty()
        {
            
            var command = new CreateCustomerCommand
            {
                Customer = new CustomerDto
                {
                    FullName = "John Doe",
                    Address = "Rua Teste",
                    Phone = "+55 11 99999-9999",
                    Email = "",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
                }
            };

            
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Equal("Email is required.", result.Errors[0].ErrorMessage);
        }

        
        [Fact]
        public void Should_have_error_when_date_of_birth_is_in_the_future()
        {
            
            var command = new CreateCustomerCommand
            {
                Customer = new CustomerDto
                {
                    FullName = "John Doe",
                    Address = "Rua Teste",
                    Phone = "+55 11 99999-9999",
                    Email = "test@test.com",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(20))
                }
            };

            
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Equal("Date of Birth should be in the past.", result.Errors[0].ErrorMessage);
        }

    }
}
