using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.Registration.Specifications;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class SpecialtySpecificationTests
    {
        [Theory]
        [InlineData("Pediatria")]
        [InlineData("Cirurgia Torácica")]
        public void Specialty_Should_Have_Valid_Description(string description)
        {
            // Arrange
            var spec = new SpecialtyShouldHaveDescriptionSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Specialty()
            {
                Description = description
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void Specialty_Should_Have_Invalid_Description(string description)
        {
            // Arrange
            var spec = new SpecialtyShouldHaveDescriptionSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Specialty()
            {
                Description = description
            });

            // Assert
            Assert.False(isSatisfied);
        }
    }
}
