using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.Registration.Specifications;
using Tnf.Architecture.Dto.ValueObjects;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class ProfessionalSpecificationTests
    {
        [Theory]
        [InlineData("George Washington")]
        public void Professional_Should_Have_Name(string name)
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
        [InlineData("Rua do comercio")]
        public void Professional_Should_Have_Address(string street)
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
        [InlineData("123")]
        public void Professional_Should_Have_Address_Number(string number)
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
        [InlineData("APT 123")]
        public void Professional_Should_Have_Address_Complement(string complement)
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
        [InlineData("email@email.com")]
        public void Professional_Should_Have_Email(string email)
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
        [InlineData("12345678")]
        public void Professional_Should_Have_Phone(string phone)
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
        [InlineData("12345678")]
        public void Professional_Should_Have_ZipCode(string zipCode)
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
    }
}
