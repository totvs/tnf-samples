using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Store.Get;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Get;

public class GetStoreCommandHandlerTests
{
    [Fact]
    public async Task GetStoreCommandHandler_Should_Return_Store_When_StoreId_Is_Provided()
    {
        var storeRepositoryMock = new Mock<IStoreRepository>();
        storeRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Store(Guid.NewGuid(), "Test Store", "cnpj", "Test Location"));

        var loggerMock = new Mock<ILogger<GetStoreCommandHandler>>();

        var handler = new GetStoreCommandHandler(loggerMock.Object, storeRepositoryMock.Object);


        var result = await handler.ExecuteAsync(new GetStoreCommand(Guid.NewGuid()));


        Assert.NotNull(result);
        Assert.NotNull(result.Store);
        Assert.Equal("Test Store", result.Store.Name);
        Assert.Equal("Test Location", result.Store.Location);
    }

    [Fact]
    public async Task GetStoreCommandHandler_Should_Return_Stores_When_StoreId_Is_Not_Provided()
    {
        var storeRepositoryMock = new Mock<IStoreRepository>();
        storeRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Domain.Entities.Store>
            {
                new(Guid.NewGuid(), "Test Store 1", "cnpj", "Test Location 1"),
                new(Guid.NewGuid(), "Test Store 2", "cnpj", "Test Location 2")
            });

        var loggerMock = new Mock<ILogger<GetStoreCommandHandler>>();

        var handler = new GetStoreCommandHandler(loggerMock.Object, storeRepositoryMock.Object);


        var result = await handler.ExecuteAsync(new GetStoreCommand());


        Assert.NotNull(result);
        Assert.NotNull(result.Stores);
        Assert.Equal(2, result.Stores.Count);
        Assert.Equal("Test Store 1", result.Stores[0].Name);
        Assert.Equal("Test Location 1", result.Stores[0].Location);
        Assert.Equal("Test Store 2", result.Stores[1].Name);
        Assert.Equal("Test Location 2", result.Stores[1].Location);
    }
}