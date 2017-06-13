using Tnf.Architecture.Domain.WhiteHouse;
using Xunit;
using System.Linq;
using Tnf.App.Bus.Notifications;

namespace Tnf.Architecture.Domain.Tests.WhiteHouse
{
    public class PresidentBuilderTests
    {
        [Fact]
        public void Create_Valid_President()
        {
            // Arrange
            var builder = new PresidentBuilder()
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress("Rua de teste", "123", "APT 12", "99380000");

            // Act
            var build = builder.Build();

            // Assert
            Assert.False(Notification.HasNotification());
        }

        [Fact]
        public void Create_President_With_Invalid_Name()
        {
            // Arrange
            var builder = new PresidentBuilder()
                  .WithId("1")
                  .WithName(null)
                  .WithAddress("Rua de teste", "123", "APT 12", "99380000");

            // Act
            var build = builder.Build();

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_President_With_Invalid_ZipCode()
        {
            // Arrange
            var builder = new PresidentBuilder()
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress("Rua de teste", "123", "APT 12", "993800021120");

            // Act
            var build = builder.Build();

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_President_With_Invalid_Address()
        {
            // Arrange
            var builder = new PresidentBuilder()
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress(null, "123", "APT 12", "99380000");

            // Act
            var build = builder.Build();

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_President_With_Invalid_Address_Number()
        {
            // Arrange
            var builder = new PresidentBuilder()
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress("Rua de teste", null, "APT 12", "99380000");

            // Act
            var build = builder.Build();

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressNumberMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_President_With_Invalid_Address_Complement()
        {
            // Arrange
            var builder = new PresidentBuilder()
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress("Rua de teste", "123", null, "99380000");

            // Act
            var build = builder.Build();

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
        }
    }
}
