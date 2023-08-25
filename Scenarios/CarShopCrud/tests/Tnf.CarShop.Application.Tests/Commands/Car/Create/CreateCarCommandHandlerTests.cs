using Microsoft.Extensions.Logging;

using Moq;

using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Car.Create;

public class CreateCarCommandHandlerTests
{
    private readonly Mock<ICarRepository> _carRepoMock;
    private readonly CreateCarCommandHandler _handler;
    private readonly Mock<ILogger<CreateCarCommandHandler>> _loggerMock;
    private readonly Mock<ICarEventPublisher> _carEventPublisherMock;

    public CreateCarCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateCarCommandHandler>>();
        _carRepoMock = new Mock<ICarRepository>();
        _carEventPublisherMock = new Mock<ICarEventPublisher>();

        _handler = new CreateCarCommandHandler(_loggerMock.Object, _carRepoMock.Object, _carEventPublisherMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ValidCommand_CreatesCarSuccessfully()
    {
        var storeId = Guid.NewGuid();
        var createdCar = new Domain.Entities.Car("Tesla", "Model S", 2022, 79999, storeId);

        _carRepoMock.Setup(c => c.InsertAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCar);

        var command = new CreateCarCommand
        {
            Brand = "Tesla",
            Model = "Model S",
            Year = 2022,
            Price = 79999,
            StoreId = storeId
        };

        var result = await _handler.ExecuteAsync(command);

        Assert.True(result.Success);
        Assert.Equal(createdCar.Id, result.CarId);
        _carRepoMock.Verify(c => c.InsertAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
