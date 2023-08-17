using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Store.Update;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Update
{
    public class UpdateStoreCommandHandlerTests
    {
        [Fact]
        public async Task UpdateStoreCommandHandler_Should_Update_Store()
        {
            
            var storeRepositoryMock = new Mock<IStoreRepository>();
            var loggerMock = new Mock<ILogger<UpdateStoreCommandHandler>>();
            var command = new UpdateStoreCommand(Guid.NewGuid(), "Store Name", "Store Location", "bem bacana um cnpj maneiro");
            var store = new Domain.Entities.Store(command.Id,command.Cnpj ,command.Name, command.Location);
            storeRepositoryMock.Setup(s => s.GetAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(store);
            storeRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>())).ReturnsAsync(store);
            var handler = new UpdateStoreCommandHandler(loggerMock.Object, storeRepositoryMock.Object);

            
            var result = await handler.ExecuteAsync(command);

            
            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Store.Name);
            Assert.Equal(command.Location, result.Store.Location);
            storeRepositoryMock.Verify(s => s.GetAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
            storeRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
