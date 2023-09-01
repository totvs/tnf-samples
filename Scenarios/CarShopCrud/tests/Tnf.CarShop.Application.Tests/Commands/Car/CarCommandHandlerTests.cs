using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Car;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Car;
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

    [Fact]
    public async Task UpdateCarCommandHandler_Should_Update_Car()
    {
        var command = new CarCommand
        {

            Id = Guid.NewGuid(),
            Brand = "Ford",
            Model = "Fiesta",
            Year = 2019,
            Price = 20000
        };

        var car = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price, Guid.NewGuid()) { Id = (Guid)command.Id };

        _carRepoMock.Setup(x => x.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(car);
        _carRepoMock.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(car);

        var handler = new CarCommandHandler(_carRepoMock.Object, _loggerMock.Object, _carEventPublisherMock.Object);

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(command.Id, result.CarDto.Id);
        Assert.Equal(command.Brand, result.CarDto.Brand);
        Assert.Equal(command.Model, result.CarDto.Model);
        Assert.Equal(command.Year, result.CarDto.Year);
        Assert.Equal(command.Price, result.CarDto.Price);

        _carRepoMock.Verify(x => x.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _carRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
