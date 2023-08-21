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
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly CreateCustomerCommandHandler _handler;

        public CreateCustomerCommandHandlerTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _handler = new CreateCustomerCommandHandler(_customerRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ValidCommand_CreatesCustomerSuccessfully()
        {
            var customerId = Guid.NewGuid();
            var customer = new Domain.Entities.Customer(customerId, "Joao da Silva", "Rua Bem-te-vi", "999999", "joao@silva.zeh", DateTime.Now.AddYears(-33));


            _customerRepoMock.Setup(c => c.InsertAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            var command = new CreateCustomerCommand
            {
                FullName = "John Doe",
                Address = "123 Main St.",
                Phone = "555-5555",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.UtcNow.AddYears(-30)
            };

            var result = await _handler.ExecuteAsync(command);
            
            Assert.True(result.Success);
            Assert.Equal(customerId, result.CustomerId);
            _customerRepoMock.Verify(c => c.InsertAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
