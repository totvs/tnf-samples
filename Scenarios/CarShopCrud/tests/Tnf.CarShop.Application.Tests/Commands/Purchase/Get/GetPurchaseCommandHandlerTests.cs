using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.CarShop.Application.Commands.Purchase.Get;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Get
{
    public class GetPurchaseCommandHandlerTests
    {
        [Fact]
        public async Task ExecuteAsync_PurchaseIdProvided_ReturnsSinglePurchaseDto()
        {
            var purchaseId = Guid.NewGuid();
            var carId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            var car = new Domain.Entities.Car(carId, "Ford", "Fiesta", 2019, 20000, null, Guid.NewGuid());
            var customer = new Domain.Entities.Customer(customerId, "Joao da Silva", "Rua Bem-te-vi", "999999", "joao@silva.zeh", DateTime.Now.AddYears(-33));
            var purchase = new Domain.Entities.Purchase(purchaseId, carId, customerId, 100, DateTime.UtcNow, Guid.NewGuid(), customer, car, null);

            var loggerMock = new Mock<ILogger<GetPurchaseCommandHandler>>();
            var purchaseRepoMock = new Mock<IPurchaseRepository>();

            purchaseRepoMock.Setup(x => x.GetAsync(purchaseId, It.IsAny<CancellationToken>())).ReturnsAsync(purchase);

            var handler = new GetPurchaseCommandHandler(loggerMock.Object, purchaseRepoMock.Object);

            var command = new GetPurchaseCommand { PurchaseId = purchaseId };

            var result = await handler.ExecuteAsync(command);

            Assert.NotNull(result);
            Assert.NotNull(result.Purchase);
        }

        [Fact]
        public async Task ExecuteAsync_NoPurchaseIdProvided_ReturnsAllPurchases()
        {

            var purchaseId = Guid.NewGuid();
            var carId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            var car = new Domain.Entities.Car(carId, "Ford", "Fiesta", 2019, 20000, null, Guid.NewGuid());
            var customer = new Domain.Entities.Customer(customerId, "Joao da Silva", "Rua Bem-te-vi", "999999", "joao@silva.zeh", DateTime.Now.AddYears(-33));
            var purchase = new Domain.Entities.Purchase(purchaseId, carId, customerId, 100, DateTime.UtcNow, Guid.NewGuid(), customer, car, null);


            var purchases = new List<Domain.Entities.Purchase>
            {
                purchase, purchase
            };

            var loggerMock = new Mock<ILogger<GetPurchaseCommandHandler>>();
            var purchaseRepoMock = new Mock<IPurchaseRepository>();

            purchaseRepoMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(purchases);

            var handler = new GetPurchaseCommandHandler(loggerMock.Object, purchaseRepoMock.Object);

            var command = new GetPurchaseCommand();

            var result = await handler.ExecuteAsync(command);

            Assert.NotNull(result);
            Assert.Equal(2, result.Purchases.Count());
        }

        [Fact]
        public async Task ExecuteAsync_PurchaseIdProvidedButNotFound_ThrowsException()
        {
            var purchaseId = Guid.NewGuid();

            var loggerMock = new Mock<ILogger<GetPurchaseCommandHandler>>();
            var purchaseRepoMock = new Mock<IPurchaseRepository>();

            purchaseRepoMock.Setup(x => x.GetAsync(purchaseId, It.IsAny<CancellationToken>())).ReturnsAsync((Domain.Entities.Purchase)null);

            var handler = new GetPurchaseCommandHandler(loggerMock.Object, purchaseRepoMock.Object);

            var command = new GetPurchaseCommand { PurchaseId = purchaseId };

            await Assert.ThrowsAsync<Exception>(() => handler.ExecuteAsync(command));
        }
    }
}
