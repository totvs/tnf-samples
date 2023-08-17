using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Store.Delete;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Delete
{
    public class DeleteStoreCommandHandlerTests
    {
        [Fact]
        public async Task DeleteStoreCommandHandler_Should_Delete_Store()
        {
            
            var storeId = Guid.NewGuid();
            var command = new DeleteStoreCommand(storeId);
            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.Setup(s => s.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            var handler = new DeleteStoreCommandHandler(Mock.Of<ILogger<DeleteStoreCommandHandler>>(), storeRepositoryMock.Object);

            
            var result = await handler.ExecuteAsync(command);

            
            Assert.True(result.Success);
            storeRepositoryMock.Verify(s => s.DeleteAsync(storeId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
