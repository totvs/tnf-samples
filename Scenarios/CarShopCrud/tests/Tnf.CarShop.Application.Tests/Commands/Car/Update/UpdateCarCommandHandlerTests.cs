using Microsoft.Extensions.Logging;

using Moq;

using Tnf.CarShop.Application.Commands.Car.Update;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Car.Update;

public class UpdateCarCommandHandlerTests
{
    [Fact]
    public async Task UpdateCarCommandHandler_Should_Update_Car()
    {
        var carRepositoryMock = new Mock<ICarRepository>();
        var loggerMock = new Mock<ILogger<UpdateCarCommandHandler>>();
        var carEventPublisherMock = new Mock<ICarEventPublisher>();

        var command = new UpdateCarCommand
        {
            
            Id = Guid.NewGuid(),
            Brand = "Ford",
            Model = "Fiesta",
            Year = 2019,
            Price = 20000
        };

        var car = new Domain.Entities.Car(command.Id, command.Brand, command.Model, command.Year, command.Price);
        carRepositoryMock.Setup(x => x.GetAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(car);
        carRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(car);

        var handler = new UpdateCarCommandHandler(loggerMock.Object, carRepositoryMock.Object, carEventPublisherMock.Object);


        var result = await handler.ExecuteAsync(command);


        Assert.NotNull(result);
        Assert.Equal(command.Id, result.Car.Id);
        Assert.Equal(command.Brand, result.Car.Brand);
        Assert.Equal(command.Model, result.Car.Model);
        Assert.Equal(command.Year, result.Car.Year);
        Assert.Equal(command.Price, result.Car.Price);

        carRepositoryMock.Verify(x => x.GetAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        carRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
