using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Customer.Get;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Get
{
    public class GetCustomerCommandHandlerTests
    {
        [Fact]
        public async Task GetCustomerCommandHandler_Should_Return_Customer_When_Id_Is_Provided()
        {
            
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Entities.Customer(Guid.NewGuid(), "John Doe", "123 Main Street", "1234567890", "john@doe.com", DateTime.Now));

            var handler = new GetCustomerCommandHandler(customerRepositoryMock.Object);

            
            var result = await handler.ExecuteAsync(new GetCustomerCommand { CustomerId = Guid.NewGuid() });

            
            Assert.NotNull(result);
            Assert.NotNull(result.Customer);
            Assert.Equal("John Doe", result.Customer.FullName);
        }

        [Fact]
        public async Task GetCustomerCommandHandler_Should_Return_All_Customers_When_No_Id_Is_Provided()
        {
            
            var customers = new List<Domain.Entities.Customer>
            {
                new(Guid.NewGuid(), "John Doe", "123 Main Street", "1234567890", "john@doe.com", DateTime.Now),
                new(Guid.NewGuid(), "Jane Doe", "456 Main Street", "0987654321", "jane@doe.com", DateTime.Now)
            };

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(customers);

            var handler = new GetCustomerCommandHandler(customerRepositoryMock.Object);

            
            var result = await handler.ExecuteAsync(new GetCustomerCommand());

            
            Assert.NotNull(result);
            Assert.NotNull(result.Customers);
            Assert.Equal(2, result.Customers.Count);
            Assert.Equal("John Doe", result.Customers[0].FullName);
            Assert.Equal("Jane Doe", result.Customers[1].FullName);
        }

    }
}
