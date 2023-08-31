using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Car;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;
namespace Tnf.CarShop.Application.Tests.Commands.Car.Create;

public class CarCommandHandlerTests
{
    private readonly Mock<ICarRepository> _carRepoMock;
    private readonly CarCommandHandler _handler;
    private readonly Mock<ILogger<CarCommandHandler>> _loggerMock;
    private readonly Mock<ICarEventPublisher> _carEventPublisherMock;

    public CarCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CarCommandHandler>>();
        _carRepoMock = new Mock<ICarRepository>();
        _carEventPublisherMock = new Mock<ICarEventPublisher>();

        _handler = new CarCommandHandler(_carRepoMock.Object, _loggerMock.Object, _carEventPublisherMock.Object);
    }

    [Fact]
    public async Task Should_Create_Car_Successfully()
    {
        var command = new CarCommand
        {
            Brand = "Ford",
            Model = "Fiesta",
            Year = 2020,
            Price = 25000,
            StoreId = Guid.NewGuid()
        };

        _carRepoMock
            .Setup(c => c.InsertAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price, command.StoreId) { Id = Guid.NewGuid() });

        _carEventPublisherMock
            .Setup(c => c.NotifyCreationAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.ExecuteAsync(command);

        Assert.NotEqual(Guid.Empty, result.CarDto.Id);

        _carRepoMock.Verify(c => c.InsertAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()), Times.Once);
        _carEventPublisherMock.Verify(c => c.NotifyCreationAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
