﻿using Microsoft.Extensions.Logging;
using Moq;
using Tnf.CarShop.Application.Commands.Purchase;
using Tnf.CarShop.Domain.Repositories;

namespace Tnf.CarShop.Application.Tests.Commands.Purchase.Update;

public class UpdatePurchaseCommandHandlerTests
{

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

        var loggerMock = new Mock<ILogger<PurchaseCommandHandler>>();

        var command = new PurchaseCommand
        {
            Id = purchaseId,
            PurchaseDate = purchaseDate,
            CustomerId = customerId,
            CarId = carId,
            StoreId = storeId,
            Price = 23000
        };

        var handler = new PurchaseCommandHandler(
            carRepositoryMock.Object,
            customerRepositoryMock.Object,
            loggerMock.Object,
            purchaseRepositoryMock.Object,
            storeRepositoryMock.Object);

        var result = await handler.ExecuteAsync(command);

        Assert.NotNull(result);
        Assert.Equal(purchaseId, result.PurchaseDto.Id);
        Assert.Equal(purchaseDate, result.PurchaseDto.PurchaseDate);
        //Assert.Equal(customerId, result.PurchaseDto.Customer.Id);
        //Assert.Equal(carId, result.PurchaseDto.Car.Id);
        //Assert.Equal(storeId, result.PurchaseDto.Store.Id);

        purchaseRepositoryMock.Verify(x => x.GetAsync(purchaseId, It.IsAny<CancellationToken>()), Times.Between(0, 2, Moq.Range.Inclusive));
        purchaseRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Purchase>(), It.IsAny<CancellationToken>()), Times.Once);
        customerRepositoryMock.Verify(x => x.GetAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
        carRepositoryMock.Verify(x => x.GetAsync(carId, It.IsAny<CancellationToken>()), Times.Once);
        storeRepositoryMock.Verify(x => x.GetAsync(storeId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
