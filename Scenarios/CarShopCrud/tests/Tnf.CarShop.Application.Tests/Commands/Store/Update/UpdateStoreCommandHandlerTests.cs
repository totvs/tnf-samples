using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Store;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Update;

public class UpdateStoreCommandHandlerTests
{
    [Fact]
    public async Task UpdateStoreCommandHandler_Should_Update_Store()
    {
        var storeRepositoryMock = new Mock<IStoreRepository>();

        var loggerMock = new Mock<ILogger<StoreCommandHandler>>();

        var command = new StoreCommand
        {
            Id = Guid.NewGuid(),
            Name = "Store Name",
            Location = "Store Location",
            Cnpj = "bem bacana um cnpj maneiro"
        };

        var store = new Domain.Entities.Store(command.Cnpj, command.Name, command.Location);

        storeRepositoryMock.Setup(s => s.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(store);
        storeRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(store);

        var handler = new StoreCommandHandler(loggerMock.Object, storeRepositoryMock.Object);

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(command.Name, result.StoreDto.Name);
        Assert.Equal(command.Location, result.StoreDto.Location);
        storeRepositoryMock.Verify(s => s.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>()), Times.Once);
        storeRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
