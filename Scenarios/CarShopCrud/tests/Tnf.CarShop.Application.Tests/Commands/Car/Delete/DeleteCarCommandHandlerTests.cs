using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Car.Delete;

public class DeleteCarCommandHandlerTests
{
    //[Fact]
    //public async Task DeleteCarCommandHandler_Should_DeleteCar()
    //{
    //    var carRepositoryMock = new Mock<ICarRepository>();
    //    carRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
    //        .Returns(Task.FromResult(true));

    //    var loggerMock = new Mock<ILogger<DeleteCarCommandHandler>>();

    //    var command = new DeleteCarCommand(Guid.NewGuid());

    //    var handler = new DeleteCarCommandHandler(loggerMock.Object, carRepositoryMock.Object);


    //    var result = await handler.ExecuteAsync(command);

    //    Assert.True(result.Success);
    //    carRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    //}
}
