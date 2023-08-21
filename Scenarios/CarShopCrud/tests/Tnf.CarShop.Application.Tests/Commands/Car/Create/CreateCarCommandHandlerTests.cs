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
        private readonly Mock<ILogger<CreateCarCommandHandler>> _loggerMock;
        private readonly Mock<ICarRepository> _carRepoMock;
        private readonly Mock<IStoreRepository> _storeRepoMock;
        private readonly CreateCarCommandHandler _handler;

        public CreateCarCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<CreateCarCommandHandler>>();
            _carRepoMock = new Mock<ICarRepository>();
            _storeRepoMock = new Mock<IStoreRepository>();

            _handler = new CreateCarCommandHandler(_loggerMock.Object, _carRepoMock.Object, _storeRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ValidCommand_CreatesCarSuccessfully()
        {            
            var tenantId = Guid.NewGuid();
            var carId = Guid.NewGuid();
            var store = new Domain.Entities.Store(tenantId, "Loja do Zé", "000000000000", "Tramandai");
            var createdCar = new Domain.Entities.Car(carId, "Tesla", "Model S", 2022, 79999, store, tenantId);

            _storeRepoMock.Setup(s => s.GetAsync(tenantId, It.IsAny<CancellationToken>())).ReturnsAsync(store);
            _carRepoMock.Setup(c => c.InsertAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>())).ReturnsAsync(createdCar);

            var command = new CreateCarCommand
            (
                 "Tesla",
                 "Model S",
                 2022,
                79999,
                 tenantId
      
            );

            var result = await _handler.ExecuteAsync(command);

            
            Assert.True(result.Success);
            Assert.Equal(createdCar.Id, result.CarId);
            _storeRepoMock.Verify(s => s.GetAsync(tenantId, It.IsAny<CancellationToken>()), Times.Once);
            _carRepoMock.Verify(c => c.InsertAsync(It.IsAny<Domain.Entities.Car>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }

    
}
