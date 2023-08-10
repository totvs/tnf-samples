using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Customer.Get;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Get
{
    public class GetCustomerCommandValidatorTests
    {

        [Fact]
        public void GetCustomerCommandValidator_Should_Return_Error_When_CustomerId_Is_Empty()
        {
            
            var command = new GetCustomerCommand { CustomerId = Guid.Empty };
            var validator = new GetCustomerCommandValidator();

            
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorMessage == "CustomerId should not be an empty GUID.");
        }
    }
}
