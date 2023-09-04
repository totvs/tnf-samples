using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Store;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store;

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

    [Fact]
    public async Task UpdateStoreCommandHandler_Should_Update_Store()
    {                
        var command = new StoreCommand
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

        var result = await _handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(command.Name, result.StoreDto.Name);
        Assert.Equal(command.Location, result.StoreDto.Location);
        _storeRepositoryMock.Verify(s => s.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _storeRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
