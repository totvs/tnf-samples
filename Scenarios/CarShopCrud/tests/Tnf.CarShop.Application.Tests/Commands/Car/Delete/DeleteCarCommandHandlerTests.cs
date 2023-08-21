using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Car.Delete;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Tests.Commands.Car.Delete;

public class DeleteCarCommandHandlerTests
{
    [Fact]
    public async Task DeleteCarCommandHandler_Should_DeleteCar()
    {
        var carRepositoryMock = new Mock<ICarRepository>();
        carRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

        var loggerMock = new Mock<ILogger<DeleteCarCommandHandler>>();

        var command = new DeleteCarCommand(Guid.NewGuid());
        var context = new CommandContext<DeleteCarCommand, DeleteCarResult>(command);

        var handler = new DeleteCarCommandHandler(loggerMock.Object, carRepositoryMock.Object);


        await handler.HandleAsync(context);


        Assert.True(context.Result.Success);
        carRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}