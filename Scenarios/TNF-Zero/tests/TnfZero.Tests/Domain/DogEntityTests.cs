using FluentAssertions;
using TnfZero.Domain.Entities;
using Xunit;

namespace TnfZero.Tests.Domain;

public class DogEntityTests
{
    [Fact]
    public void Constructor_ShouldSetName()
    {
        // Act
        var dog = new DogEntity("Rex");

        // Assert
        dog.Name.Should().Be("Rex");
    }

    [Fact]
    public void Constructor_ShouldGenerateNonEmptyId()
    {
        // Act
        var dog = new DogEntity("Buddy");

        // Assert
        dog.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void TwoEntities_ShouldHaveDifferentIds()
    {
        // Act
        var dog1 = new DogEntity("Rex");
        var dog2 = new DogEntity("Buddy");

        // Assert
        dog1.Id.Should().NotBe(dog2.Id);
    }
}