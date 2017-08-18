using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.Registration.Specifications;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class PersonSpecificationTests
    {
        [Theory]
        [InlineData("John")]
        [InlineData("Mary")]
        public void Person_Should_Have_Valid_Name(string name)
        {
            // Arrange
            var spec = new PersonShouldHaveNameSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Person()
            {
                Name = name
            });

            // Assert
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void Person_Should_Have_Invalid_Name(string name)
        {
            // Arrange
            var spec = new PersonShouldHaveNameSpecification();

            // Act
            var isSatisfied = spec.IsSatisfiedBy(new Person()
            {
                Name = name
            });

            // Assert
            Assert.False(isSatisfied);
        }
    }
}
