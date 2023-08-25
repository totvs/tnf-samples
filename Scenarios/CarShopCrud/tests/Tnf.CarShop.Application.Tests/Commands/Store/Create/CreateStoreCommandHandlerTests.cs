using Moq;
using Tnf.CarShop.Application.Commands.Store.Create;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Create;

public class CreateStoreCommandHandlerTests
{
    private readonly CreateStoreCommandHandler _handler;
    private readonly Mock<IStoreRepository> _storeRepositoryMock;

    public CreateStoreCommandHandlerTests()
    {
        _storeRepositoryMock = new Mock<IStoreRepository>();
        _handler = new CreateStoreCommandHandler(_storeRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Create_Store()
    {
        var command = new CreateStoreCommand
        {
            Name = "Store 1",
            Cnpj = "123456789",
            Location= "Location 1"
        };

        var expectedId = Guid.NewGuid();

        _storeRepositoryMock
            .Setup(sr => sr.InsertAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Store(command.Name, command.Cnpj, command.Location));

        var result = await _handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(expectedId, result.StoreId);
    }
}
