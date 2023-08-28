using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Purchase.Get;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Get;

public class GetPurchaseCommandHandlerTests
{
    [Fact]
    public async Task ExecuteAsync_PurchaseIdProvided_ReturnsSinglePurchaseDto()
    {
        var purchaseId = Guid.NewGuid();
        var carId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        var car = new Domain.Entities.Car("Ford", "Fiesta", 2019, 20000, carId);
        var customer = new Domain.Entities.Customer("Joao da Silva", "Rua Bem-te-vi", "999999", "joao@silva.zeh", DateTime.Now.AddYears(-33), customerId);

        var purchase = new Domain.Entities.Purchase(carId, customerId, 100, DateTime.UtcNow, purchaseId)
        {
            Id = purchaseId,
        };

        var loggerMock = new Mock<ILogger<GetPurchaseCommandHandler>>();
        var purchaseRepoMock = new Mock<IPurchaseRepository>();


        var purchaseDto = new PurchaseDto
        {
            Id = purchaseId,
        };

        purchaseRepoMock.Setup(x => x.GetPurchaseDtoAsync(purchaseId, It.IsAny<CancellationToken>())).ReturnsAsync(purchaseDto);


        var handler = new GetPurchaseCommandHandler(loggerMock.Object, purchaseRepoMock.Object);

        var command = new GetPurchaseCommand { PurchaseId = purchaseId };

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.NotNull(result.Purchase);
    }
}
