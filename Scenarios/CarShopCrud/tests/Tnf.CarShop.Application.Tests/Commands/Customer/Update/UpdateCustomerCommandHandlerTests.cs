using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Customer.Update;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Update;

public class UpdateCustomerCommandHandlerTests
{
    [Fact]
    public async Task UpdateCustomerCommandHandler_Should_Update_Customer()
    {
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var loggerMock = new Mock<ILogger<UpdateCustomerCommandHandler>>();
        var command = new UpdateCustomerCommand
        {
            Id = Guid.NewGuid(),
            StoreId = Guid.NewGuid(),
            FullName = "John Doe",
            Address = "123 Main Street",
            Phone = "1234567890",
            Email = "john@doe.com",
            DateOfBirth = new DateTime(1980, 1, 1), 
        };
        var customer = new Domain.Entities.Customer(command.FullName, command.Address, command.Phone, command.Email, command.DateOfBirth, command.StoreId);

        customerRepositoryMock.Setup(x => x.GetAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(customer);

        customerRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var handler = new UpdateCustomerCommandHandler(loggerMock.Object, customerRepositoryMock.Object);

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(command.Id, result.Customer.Id);
        Assert.Equal(command.FullName, result.Customer.FullName);
        Assert.Equal(command.Address, result.Customer.Address);
        Assert.Equal(command.Phone, result.Customer.Phone);
        Assert.Equal(command.Email, result.Customer.Email);
        Assert.Equal(command.DateOfBirth, result.Customer.DateOfBirth);
        customerRepositoryMock.Verify(x => x.GetAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        customerRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
