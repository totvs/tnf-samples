using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.Registration.Specifications;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class ProfessionalSpecificationTests
    {
        [Theory]
        [InlineData("Doutor Alberto")]
        [InlineData("Doutora Larissa")]
        public void Professional_Should_Have_Valid_Name(string name)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveNameSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Name = name
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void Professional_Should_Have_Invalid_Name(string name)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveNameSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Name = name
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("Rua do comercio")]
        [InlineData("Avenida Ipiranga")]
        public void Professional_Should_Have_Valid_Address(string street)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveAddressSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Address = new Address(street, null, null, null)
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void Professional_Should_Have_Invalid_Address(string street)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveAddressSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Address = new Address(street, null, null, null)
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("78")]
        public void Professional_Should_Have_Valid_Address_Number(string number)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveAddressNumberSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Address = new Address(null, number, null, null)
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void Professional_Should_Have_Invalid_Address_Number(string number)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveAddressNumberSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Address = new Address(null, number, null, null)
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("APT 123")]
        [InlineData("c/ 12")]
        public void Professional_Should_Have_Valid_Address_Complement(string complement)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveAddressComplementSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Address = new Address(null, null, complement, null)
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void Professional_Should_Have_Invalid_Address_Complement(string complement)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveAddressComplementSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Address = new Address(null, null, complement, null)
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("email@email.com")]
        [InlineData("teste@teste.com")]
        public void Professional_Should_Have_Valid_Email(string email)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveEmailSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Email = email
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void Professional_Should_Have_Invalid_Email(string email)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveEmailSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Email = email
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("+55 051 8765-4321")]
        public void Professional_Should_Have_Valid_Phone(string phone)
        {
            // Arrange
            var spec = new ProfessionalShouldHavePhoneSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Phone = phone
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void Professional_Should_Have_Invalid_Phone(string phone)
        {
            // Arrange
            var spec = new ProfessionalShouldHavePhoneSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Phone = phone
            });

            // Assert
            Assert.False(isSatisfied);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("99.999-999")]
        public void Professional_Should_Have_Valid_ZipCode(string zipCode)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveZipCodeSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Address = new Address(null, null, null, new ZipCode(zipCode))
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("1234567")]
        [InlineData("99.999-9899")]
        public void Professional_Should_Have_Invalid_ZipCode(string zipCode)
        {
            // Arrange
            var spec = new ProfessionalShouldHaveZipCodeSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Professional()
            {
                Address = new Address(null, null, null, new ZipCode(zipCode))
            });

            // Assert
            Assert.False(isSatisfied);
        }
    }
}
