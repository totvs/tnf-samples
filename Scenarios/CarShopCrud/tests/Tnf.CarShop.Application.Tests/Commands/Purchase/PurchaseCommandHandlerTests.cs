using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Purchase;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase;
public class PurchaseCommandHandlerTests
{
    [Fact]
    public async Task ExecuteAsync_ValidCommand_ReturnsExpectedPurchaseId()
    {
        var carId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var storeId = Guid.NewGuid();

        var store = new Domain.Entities.Store("Loja", "Cnpj", "casa");

        var car = new Domain.Entities.Car("Ford", "Fiesta", 2019, 20000, storeId);

        var customer = new Domain.Entities.Customer("Joao da Silva", "Rua Bem-te-vi", "999999", "joao@silva.zeh", DateTime.Now.AddYears(-33), storeId);

        var purchase = new Domain.Entities.Purchase(carId, customerId, 100, DateTime.UtcNow, Guid.NewGuid());

        var loggerMock = new Mock<ILogger<PurchaseCommandCreateHandler>>();
        var purchaseRepoMock = new Mock<IPurchaseRepository>();
        var carRepoMock = new Mock<ICarRepository>();
        var customerRepoMock = new Mock<ICustomerRepository>();
        var storeRepoMock = new Mock<IStoreRepository>();

        carRepoMock.Setup(x => x.GetAsync(carId, It.IsAny<CancellationToken>())).ReturnsAsync(car);
        customerRepoMock.Setup(x => x.GetAsync(customerId, It.IsAny<CancellationToken>())).ReturnsAsync(customer);
        storeRepoMock.Setup(x => x.GetAsync(storeId, It.IsAny<CancellationToken>())).ReturnsAsync(store);        

        purchase.Id = Guid.NewGuid();

        purchaseRepoMock.Setup(x => x.GetAsync(purchase.Id, It.IsAny<CancellationToken>()))
          .ReturnsAsync(purchase);
        purchaseRepoMock.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Purchase>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(purchase);

        var handler = new PurchaseCommandCreateHandler(
            carRepoMock.Object,
            customerRepoMock.Object,
            loggerMock.Object,
            purchaseRepoMock.Object,
            storeRepoMock.Object
        );

        var command = new PurchaseCommandCreate
        {
            CarId = carId,
            CustomerId = customerId,
            StoreId = storeId,
            Price = 100,
            PurchaseDate = DateTime.UtcNow
        };

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(purchase.Id, result.PurchaseDto.Id);
    }

    [Fact]
    public async Task UpdatePurchaseCommandHandler_Should_Update_Purchase()
    {
        var purchaseId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var carId = Guid.NewGuid();
        var storeId = Guid.NewGuid();
        var purchaseDate = DateTime.Now;

        var purchase = new Domain.Entities.Purchase(carId, customerId, 23002, DateTime.Now, storeId)
        {
            Id = purchaseId
        };
        purchase.UpdatePurchaseDate(purchaseDate);

        var customer = new Domain.Entities.Customer("Joao", "Rua bem-te-vi", "51 99999-9999", "joao@joao.com", DateTime.Now.AddYears(-28), storeId)
        {
            Id = customerId
        };


        purchase.UpdateCustomer(customer);

        var car = new Domain.Entities.Car("Hyundai", "Azera", 2008, 20000, storeId)
        {
            Id = carId
        };
        purchase.UpdateCar(car);

        var store = new Domain.Entities.Store("Loja do André", "0000000000000", "Xangri-la")
        {
            Id = storeId,
        };

        purchase.UpdateStore(store);

        var purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        purchaseRepositoryMock.Setup(x => x.GetAsync(purchaseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(purchase);
        purchaseRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Purchase>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(purchase);

        var customerRepositoryMock = new Mock<ICustomerRepository>();
        customerRepositoryMock.Setup(x => x.GetAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var carRepositoryMock = new Mock<ICarRepository>();
        carRepositoryMock.Setup(x => x.GetAsync(carId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(car);

        var storeRepositoryMock = new Mock<IStoreRepository>();
        storeRepositoryMock.Setup(x => x.GetAsync(storeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(store);

        var loggerMock = new Mock<ILogger<PurchaseCommandUpdateHandler>>();

        var command = new PurchaseCommandUpdate
        {
            Id = purchaseId,
            PurchaseDate = purchaseDate,
            CustomerId = customerId,
            CarId = carId,
            StoreId = storeId,
            Price = 23000
        };

        var handler = new PurchaseCommandUpdateHandler(
            carRepositoryMock.Object,
            customerRepositoryMock.Object,
            loggerMock.Object,
            purchaseRepositoryMock.Object,
            storeRepositoryMock.Object);

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(purchaseId, result.PurchaseDto.Id);
        Assert.Equal(purchaseDate, result.PurchaseDto.PurchaseDate);

        purchaseRepositoryMock.Verify(x => x.GetAsync(purchaseId, It.IsAny<CancellationToken>()), Times.Between(0, 2, Moq.Range.Inclusive));
        purchaseRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Purchase>(), It.IsAny<CancellationToken>()), Times.Once);
        customerRepositoryMock.Verify(x => x.GetAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
        carRepositoryMock.Verify(x => x.GetAsync(carId, It.IsAny<CancellationToken>()), Times.Once);
        storeRepositoryMock.Verify(x => x.GetAsync(storeId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
