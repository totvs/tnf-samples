using Tnf.Architecture.Domain.Registration;
using Xunit;
using System.Linq;
using Tnf.App.TestBase;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class SpecialtyBuilderTests : TnfAppIntegratedTestBase<DomainModuleTest>
    {
        [Fact]
        public void Create_Valid_Specialty()
        {
            // Arrange
            var builder = new SpecialtyBuilder(LocalNotification)
                  .WithDescription("Pediatria");

            // Act
            var build = builder.Build();

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Create_Specialty_With_Invalid_Description()
        {
            // Arrange
            var builder = new SpecialtyBuilder(LocalNotification)
                  .WithDescription(null);

            // Act
            var build = builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }
    }
}
