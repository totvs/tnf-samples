using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Car.Create
{
    public class CreateCarCommandHandlerTests
    {
        [Fact]
        public async Task CreateCarCommandHandler_ShouldCreateCar()
        {            
            var carRepositoryMock = new Mock<ICarRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var dealerRepositoryMock = new Mock<IStoreRepository>();
            var loggerMock = new Mock<ILogger<CreateCarCommandHandler>>();

            var command = new CreateCarCommand("Ford", "Fiesta", 2019, 20000);
            var handler = new CreateCarCommandHandler(loggerMock.Object, carRepositoryMock.Object,
                dealerRepositoryMock.Object, customerRepositoryMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotEqual(Guid.Empty, result.CarId);
            carRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
