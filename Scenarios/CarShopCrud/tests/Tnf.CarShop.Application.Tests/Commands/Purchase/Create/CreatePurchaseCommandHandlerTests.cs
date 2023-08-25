using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Purchase.Create;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Create;

public class CreatePurchaseCommandHandlerTests
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

        var loggerMock = new Mock<ILogger<CreatePurchaseCommandHandler>>();
        var purchaseRepoMock = new Mock<IPurchaseRepository>();
        var carRepoMock = new Mock<ICarRepository>();
        var customerRepoMock = new Mock<ICustomerRepository>();
        var storeRepoMock = new Mock<IStoreRepository>();

        carRepoMock.Setup(x => x.GetAsync(carId, It.IsAny<CancellationToken>())).ReturnsAsync(car);
        customerRepoMock.Setup(x => x.GetAsync(customerId, It.IsAny<CancellationToken>())).ReturnsAsync(customer);
        storeRepoMock.Setup(x => x.GetAsync(storeId, It.IsAny<CancellationToken>())).ReturnsAsync(store);
        purchaseRepoMock.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Purchase>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(purchase);

        var handler = new CreatePurchaseCommandHandler(
            loggerMock.Object,
            purchaseRepoMock.Object,
            carRepoMock.Object,
            customerRepoMock.Object,
            storeRepoMock.Object
        );

        var command = new CreatePurchaseCommand
        {
            CarId = carId,
            CustomerId = customerId,
            StoreId = storeId,
            Price = 100,
            PurchaseDate = DateTime.UtcNow
        };

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(purchase.Id, result.PurchaseId);
    }
}
