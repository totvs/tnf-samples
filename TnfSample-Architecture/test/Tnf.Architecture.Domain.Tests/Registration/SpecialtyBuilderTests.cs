using Tnf.Architecture.Domain.Registration;
using Xunit;
using System.Linq;
using Tnf.App.Bus.Notifications;

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
            Assert.False(Notification.HasNotification());
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
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }
    }
}
