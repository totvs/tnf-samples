using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Create
{
    public class CreateCustomerCommandHandlerTests
    {
        [Fact]
        public async Task CreateCustomerCommandHandler_Should_Create_Customer()
        {
            
            var customerRepositoryMock = new Mock<ICustomerRepository>();
        
            var command = new CreateCustomerCommand {
                FullName = "John Doe",
                Address = "123 Main Street",
                Phone = "51999999999",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Now.AddYears(-28)

                };
            var handler = new CreateCustomerCommandHandler(customerRepositoryMock.Object);

            
            var result = await handler.ExecuteAsync(command);

            
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotEqual(Guid.Empty, result.CustomerId);
        }
    }
}
