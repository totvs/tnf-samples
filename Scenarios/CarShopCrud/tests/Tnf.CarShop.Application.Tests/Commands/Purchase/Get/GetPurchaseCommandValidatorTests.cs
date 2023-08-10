using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Purchase.Get;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Get
{
    public class GetPurchaseCommandValidatorTests
    {
        [Fact]
        public void GetPurchaseCommandValidator_Should_Return_Error_When_PurchaseId_Is_Empty()
        {
            
            var command = new GetPurchaseCommand { PurchaseId = Guid.Empty };
            var validator = new GetPurchaseCommandValidator();

            
            var result = validator.Validate(command);

           
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.ErrorMessage == "PurchaseId should not be an empty GUID.");
        }
    }
}
