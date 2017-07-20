using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Domain.WhiteHouse.Specifications;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.WhiteHouse
{
    public class PresidentSpecificationsTests
    {
        [Theory]
        [InlineData("Abraham Lincon")]
        [InlineData("George Washington")]
        public void President_Should_Have_Valid_Name(string name)
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
        [InlineData("87654321")]
        [InlineData("99.999-999")]
        public void President_Should_Have_Valid_ZipCode(string zipCode)
        {
            // Arrange
            var spec = new PresidentShouldHaveZipCodeSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Address = new Address("Rua de teste", "123", "APT 12", new ZipCode(zipCode))
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("8765432")]
        [InlineData("99.999-9899")]
        [InlineData("99.9999899")]
        public void President_Not_Accept_Invalid_ZipCode(string zipCode)
        {
            // Arrange
            var spec = new PresidentShouldHaveZipCodeSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Address = new Address("Rua de teste", "123", "APT 12", new ZipCode(zipCode))
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("Rua do comercio")]
        [InlineData("Avenida Ipiranga")]
        public void President_Should_Have_Valid_Address(string street)
        {
            // Arrange
            var spec = new PresidentShouldHaveAddressSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Address = new Address(street, null, null, null)
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void President_Should_Have_Invalid_Address(string street)
        {
            // Arrange
            var spec = new PresidentShouldHaveAddressSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Address = new Address(street, null, null, null)
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("78")]
        public void President_Should_Have_Valid_Address_Number(string number)
        {
            // Arrange
            var spec = new PresidentShouldHaveAddressNumberSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Address = new Address(null, number, null, null)
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void President_Should_Have_Invalid_Address_Number(string number)
        {
            // Arrange
            var spec = new PresidentShouldHaveAddressNumberSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Address = new Address(null, number, null, null)
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("APT 123")]
        [InlineData("c/ 12")]
        public void President_Should_Havee_Valid_Address_Complement(string complement)
        {
            // Arrange
            var spec = new PresidentShouldHaveAddressComplementSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Address = new Address(null, null, complement, null)
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void President_Should_Havee_Invalid_Address_Complement(string complement)
        {
            // Arrange
            var spec = new PresidentShouldHaveAddressComplementSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new President()
            {
                Address = new Address(null, null, complement, null)
            });

            // Assert
            Assert.False(isSatisfied);
        }
    }
}
