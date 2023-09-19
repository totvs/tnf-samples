using Moq;
using Tnf.CarShop.Application.Commands.Customer;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Customer;
public class CustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepoMock;
    private readonly CustomerCommandCreateHandler _createHandler;
    private readonly CustomerCommandUpdateHandler _updateHandler;

    public CustomerCommandHandlerTests()
    {
        _customerRepoMock = new Mock<ICustomerRepository>();
        _createHandler = new CustomerCommandCreateHandler(_customerRepoMock.Object);
        _updateHandler = new CustomerCommandUpdateHandler(_customerRepoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ValidCommand_CreatesCustomerSuccessfully()
    {
        var storeId = Guid.NewGuid();
        var customer = new Domain.Entities.Customer("Joao da Silva", "Rua Bem-te-vi", "999999", "joao@silva.zeh", DateTime.Now.AddYears(-33), storeId);

        _customerRepoMock.Setup(c => c.InsertAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var command = new CustomerCommandCreateAdmin
        {
            FullName = "John Doe",
            Address = "123 Main St.",
            Phone = "555-5555",
            Email = "john.doe@example.com",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            StoreId = storeId,
            MustBeAdmin = true
        };

        var result = await _createHandler.ExecuteAsync(command);

        _customerRepoMock.Verify(c => c.InsertAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CustomerCommandHandler_Should_Update_Customer()
    {        
        var command = new CustomerCommandUpdateAdmin
        {
            Id = Guid.NewGuid(),
            StoreId = Guid.NewGuid(),
            FullName = "John Doe",
            Address = "123 Main Street",
            Phone = "1234567890",
            Email = "john@doe.com",
            DateOfBirth = new DateTime(1980, 1, 1),
            MustBeAdmin = true
        };

        var customer = new Domain.Entities.Customer(command.FullName, command.Address, command.Phone, command.Email, command.DateOfBirth, command.StoreId);

        _customerRepoMock.Setup(x => x.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(customer);

        _customerRepoMock.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var result = await _updateHandler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(command.FullName, result.CustomerDto.FullName);
        Assert.Equal(command.Address, result.CustomerDto.Address);
        Assert.Equal(command.Phone, result.CustomerDto.Phone);
        Assert.Equal(command.Email, result.CustomerDto.Email);
        Assert.Equal(command.DateOfBirth, result.CustomerDto.DateOfBirth);
        _customerRepoMock.Verify(x => x.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
