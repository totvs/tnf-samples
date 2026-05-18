using FluentAssertions;
using Moq;
using TnfZero.Application.Commands.CreateDog;
using TnfZero.Domain.Entities;
using TnfZero.Domain.Repositories;
using Xunit;

namespace TnfZero.Tests.Application;

public class CreateDogCommandHandlerTests
{
    private readonly CreateDogCommandHandler _handler;
    private readonly Mock<IDogRepository> _repositoryMock;

    public CreateDogCommandHandlerTests()
    {
        _repositoryMock = new Mock<IDogRepository>();
        _handler = new CreateDogCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldInsertEntity_AndReturnNewId()
    {
        // Arrange
        var command = new CreateDogCommand("Rex");

        _repositoryMock
            .Setup(r => r.InsertAsync(It.IsAny<DogEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DogEntity e, CancellationToken _) => e);

        // Act
        var result = await _handler.ExecuteAsync(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _repositoryMock.Verify(
            r => r.InsertAsync(It.Is<DogEntity>(e => e.Name == "Rex"), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateEntityWithCorrectName()
    {
        // Arrange
        var command = new CreateDogCommand("Buddy");
        DogEntity? capturedEntity = null;

        _repositoryMock
            .Setup(r => r.InsertAsync(It.IsAny<DogEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DogEntity e, CancellationToken _) =>
            {
                capturedEntity = e;
                return e;
            });

        // Act
        await _handler.ExecuteAsync(command, CancellationToken.None);

        // Assert
        capturedEntity.Should().NotBeNull();
        capturedEntity!.Name.Should().Be("Buddy");
        capturedEntity.Id.Should().NotBeEmpty();
    }
}