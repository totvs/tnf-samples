﻿using Moq;
using Tnf.CarShop.Application.Commands.Customer;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Customer.Create;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepoMock;
    private readonly CustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _customerRepoMock = new Mock<ICustomerRepository>();
        _handler = new CustomerCommandHandler(_customerRepoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ValidCommand_CreatesCustomerSuccessfully()
    {
        var storeId = Guid.NewGuid();
        var customer = new Domain.Entities.Customer("Joao da Silva", "Rua Bem-te-vi", "999999", "joao@silva.zeh", DateTime.Now.AddYears(-33), storeId);

        _customerRepoMock.Setup(c => c.InsertAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var command = new CustomerCommand
        {
            FullName = "John Doe",
            Address = "123 Main St.",
            Phone = "555-5555",
            Email = "john.doe@example.com",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            StoreId = storeId
        };

        var result = await _handler.ExecuteAsync(command);
        
        _customerRepoMock.Verify(c => c.InsertAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
