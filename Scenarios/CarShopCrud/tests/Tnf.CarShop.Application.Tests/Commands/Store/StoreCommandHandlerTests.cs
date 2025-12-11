using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Store;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store;

public class StoreCommandHandlerTests
{
    private readonly StoreCommandCreateHandler _createHandler;
    private readonly StoreCommandUpdateHandler _updateHandler;
    private readonly Mock<IStoreRepository> _storeRepositoryMock;
    private readonly Mock<ILogger<StoreCommandCreateHandler>> _createLoggerMock;
    private readonly Mock<ILogger<StoreCommandUpdateHandler>> _updateLoggerMock;

    public StoreCommandHandlerTests()
    {
        _storeRepositoryMock = new Mock<IStoreRepository>();

        _createLoggerMock = new Mock<ILogger<StoreCommandCreateHandler>>();        
        _createHandler = new StoreCommandCreateHandler(_createLoggerMock.Object, _storeRepositoryMock.Object);

        _updateLoggerMock = new Mock<ILogger<StoreCommandUpdateHandler>>();
        _updateHandler = new StoreCommandUpdateHandler(_updateLoggerMock.Object, _storeRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Create_Store()
    {
        var command = new StoreCommandCreate
        {
            Name = "Store 1",
            Cnpj = "123456789",
            Location = "Location 1"
        };

        var expectedId = Guid.NewGuid();

        _storeRepositoryMock
            .Setup(sr => sr.InsertAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Store(command.Name, command.Cnpj, command.Location) { Id = expectedId });

        var result = await _createHandler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(expectedId, result.StoreDto.Id);
    }

    [Fact]
    public async Task UpdateStoreCommandHandler_Should_Update_Store()
    {                
        var command = new StoreCommandUpdate
        {
            Id = Guid.NewGuid(),
            Name = "Store Name",
            Location = "Store Location",
            Cnpj = "bem bacana um cnpj maneiro"
        };

        var store = new Domain.Entities.Store(command.Cnpj, command.Name, command.Location);

        _storeRepositoryMock.Setup(s => s.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(store);
        _storeRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(store);        

        var result = await _updateHandler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(command.Name, result.StoreDto.Name);
        Assert.Equal(command.Location, result.StoreDto.Location);
        _storeRepositoryMock.Verify(s => s.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _storeRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
