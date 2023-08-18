using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Store.Create;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Store.Create
{
    public class CreateStoreCommandHandlerTests
    {
        private readonly Mock<IStoreRepository> _storeRepositoryMock;
        private readonly CreateStoreCommandHandler _handler;

        public CreateStoreCommandHandlerTests()
        {
            _storeRepositoryMock = new Mock<IStoreRepository>();
            _handler = new CreateStoreCommandHandler(_storeRepositoryMock.Object);
        }
        [Fact]
        public async Task Should_Create_Store()
        {
            
            var command = new CreateStoreCommand("Store 1", "123456789","Location 1");
            var expectedId = Guid.NewGuid();
            _storeRepositoryMock.Setup(sr => sr.InsertAsync(It.IsAny<Domain.Entities.Store>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Entities.Store(expectedId, command.Name, command.Cnpj, command.Location));
            
            var result = await _handler.ExecuteAsync(command);
            
            Assert.NotNull(result);
            Assert.Equal(expectedId, result.StoreId);
        }
    }
}
