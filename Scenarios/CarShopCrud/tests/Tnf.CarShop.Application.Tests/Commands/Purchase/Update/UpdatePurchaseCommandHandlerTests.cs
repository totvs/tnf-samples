using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Purchase.Update;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Update;

public class UpdatePurchaseCommandHandlerTests
{
    [Fact]
    public async Task ExecuteAsync_ValidCommand_UpdatesAndReturnsPurchaseDto()
    {
        var purchaseId = Guid.NewGuid();
        var carId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        var car = new Domain.Entities.Car(carId, "Ford", "Fiesta", 2019, 20000, null, Guid.NewGuid());
        var customer = new Domain.Entities.Customer(customerId, "Joao da Silva", "Rua Bem-te-vi", "999999",
            "joao@silva.zeh", DateTime.Now.AddYears(-33));
        var purchase = new Domain.Entities.Purchase(purchaseId, carId, customerId, 100, DateTime.UtcNow, Guid.NewGuid(),
            customer, car, null);

        var loggerMock = new Mock<ILogger<UpdatePurchaseCommandHandler>>();
        var purchaseRepoMock = new Mock<IPurchaseRepository>();
        var carRepoMock = new Mock<ICarRepository>();
        var customerRepoMock = new Mock<ICustomerRepository>();

        purchaseRepoMock.Setup(x => x.UpdateAsync(purchase, It.IsAny<CancellationToken>())).ReturnsAsync(purchase);
        purchaseRepoMock.Setup(x => x.GetAsync(purchaseId, It.IsAny<CancellationToken>())).ReturnsAsync(purchase);
        customerRepoMock.Setup(x => x.GetAsync(customerId, It.IsAny<CancellationToken>())).ReturnsAsync(customer);
        carRepoMock.Setup(x => x.GetAsync(carId, It.IsAny<CancellationToken>())).ReturnsAsync(car);

        var handler = new UpdatePurchaseCommandHandler(
            loggerMock.Object,
            purchaseRepoMock.Object,
            customerRepoMock.Object,
            carRepoMock.Object
        );

        var command = new UpdatePurchaseCommand
        {
            Id = purchaseId,
            CarId = carId,
            CustomerId = customerId
        };

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(purchaseId, result.Purchase.Id);
    }
}