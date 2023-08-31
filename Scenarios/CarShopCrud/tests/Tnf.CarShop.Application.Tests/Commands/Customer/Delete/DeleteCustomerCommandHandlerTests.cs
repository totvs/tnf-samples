namespace Tnf.CarShop.Application.Tests.Commands.Customer.Delete;

public class DeleteCustomerCommandHandlerTests
{
    //[Fact]
    //public async Task DeleteCustomerCommandHandler_Should_Delete_Customer()
    //{
    //    var customerId = Guid.NewGuid();
    //    var command = new DeleteCustomerCommand { CustomerId = customerId };
    //    var customerRepositoryMock = new Mock<ICustomerRepository>();

    //    customerRepositoryMock.Setup(c => c.DeleteAsync(customerId, It.IsAny<CancellationToken>()))
    //        .Returns(Task.CompletedTask);

    //    var loggerMock = new Mock<ILogger<DeleteCustomerCommandHandler>>();

    //    var handler = new DeleteCustomerCommandHandler(loggerMock.Object, customerRepositoryMock.Object);

    //    var result = await handler.ExecuteAsync(command);

    //    Assert.True(result.Success);
    //    customerRepositoryMock.Verify(c => c.DeleteAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
    //}
}
