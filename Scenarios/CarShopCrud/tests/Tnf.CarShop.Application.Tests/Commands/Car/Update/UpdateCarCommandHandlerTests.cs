using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Car;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Car.Update;

public class UpdateCarCommandHandlerTests
{
    [Fact]
    public async Task UpdateCarCommandHandler_Should_Update_Car()
    {
        var carRepositoryMock = new Mock<ICarRepository>();
        var loggerMock = new Mock<ILogger<CarCommandHandler>>();
        var carEventPublisherMock = new Mock<ICarEventPublisher>();

        var command = new CarCommand
        {

            Id = Guid.NewGuid(),
            Brand = "Ford",
            Model = "Fiesta",
            Year = 2019,
            Price = 20000
        };

        var car = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price, Guid.NewGuid()) { Id = (Guid)command.Id };

        carRepositoryMock.Setup(x => x.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(car);
        carRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(car);

        var handler = new CarCommandHandler(carRepositoryMock.Object, loggerMock.Object, carEventPublisherMock.Object);

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(command.Id, result.CarDto.Id);
        Assert.Equal(command.Brand, result.CarDto.Brand);
        Assert.Equal(command.Model, result.CarDto.Model);
        Assert.Equal(command.Year, result.CarDto.Year);
        Assert.Equal(command.Price, result.CarDto.Price);

        carRepositoryMock.Verify(x => x.GetAsync((Guid)command.Id, It.IsAny<CancellationToken>()), Times.Once);
        carRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
