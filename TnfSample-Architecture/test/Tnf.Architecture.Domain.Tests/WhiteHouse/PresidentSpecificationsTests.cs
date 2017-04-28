using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Domain.WhiteHouse.Specifications;
using Tnf.Architecture.Dto.ValueObjects;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.WhiteHouse.Specifications
{
    public class PresidentSpecificationsTests
    {
        [Theory]
        [InlineData("Abraham Lincon")]
        [InlineData("George Washington")]
        public void President_Should_Have_Name(string name)
        {
            // Arrange
            var spec = new PresidentShouldHaveNameSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Name = name
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void President_Not_Accept_Invalid_Name(string name)
        {
            // Arrange
            var spec = new PresidentShouldHaveNameSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Name = name
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("1234567")]
        [InlineData("8765432")]
        [InlineData("99.999-9899")]
        [InlineData("99.9999899")]
        public void President_Not_Accept_Invalid_ZipCodes(string zipCode)
        {
            // Arrange
            var spec = new PresidentShouldHaveZipCodeSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                ZipCode = new ZipCode(zipCode)
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("87654321")]
        [InlineData("99.999-999")]
        public void President_Should_Have_Valid_ZipCode(string zipCode)
        {
            // Arrange
            var spec = new PresidentShouldHaveZipCodeSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                ZipCode = new ZipCode(zipCode)
            });

            // Assert
            Assert.True(isSatisfied);
        }
    }
}
