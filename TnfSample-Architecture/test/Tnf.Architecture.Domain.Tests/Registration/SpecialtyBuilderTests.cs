using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Xunit;
using System.Linq;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class SpecialtyBuilderTests
    {
        [Fact]
        public void Create_Valid_Specialty()
        {
            // Arrange
            var builder = new SpecialtyBuilder()
                  .WithDescription("Pediatria");

            // Act
            var build = builder.Build();

            // Assert
            Assert.True(build.Success);
        }

        [Fact]
        public void Create_Specialty_With_Invalid_Description()
        {
            // Arrange
            var builder = new SpecialtyBuilder()
                  .WithDescription(null);

            // Act
            var build = builder.Build();

            // Assert
            Assert.False(build.Success);
            Assert.True(build.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }
    }
}
