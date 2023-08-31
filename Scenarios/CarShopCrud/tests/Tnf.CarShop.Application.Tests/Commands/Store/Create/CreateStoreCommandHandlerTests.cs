using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Car;
using Tnf.CarShop.Application.Commands.Store;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Create;

public class StoreCommandHandlerTests
{
    private readonly StoreCommandHandler _handler;
    private readonly Mock<IStoreRepository> _storeRepositoryMock;
    private readonly Mock<ILogger<StoreCommandHandler>> _loggerMock;

    public StoreCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<StoreCommandHandler>>();
        _storeRepositoryMock = new Mock<IStoreRepository>();
        _handler = new StoreCommandHandler(_loggerMock.Object, _storeRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Create_Store()
    {
        var command = new StoreCommand
        {
            Name = "Store 1",
            Cnpj = "123456789",
            Location = "Location 1"
        };

        var expectedId = Guid.NewGuid();

        _storeRepositoryMock
            .Setup(sr => sr.InsertAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Store(command.Name, command.Cnpj, command.Location) { Id = expectedId });

        var result = await _handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(expectedId, result.StoreDto.Id);
    }
}
