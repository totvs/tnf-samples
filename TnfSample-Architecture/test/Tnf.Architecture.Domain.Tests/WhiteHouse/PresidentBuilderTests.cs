using System.Linq;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.WhiteHouse;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.WhiteHouse
{
    public class PresidentBuilderTests : TnfAppIntegratedTestBase<DomainModuleTest>
    {
        [Fact]
        public void Create_Valid_President()
        {
            // Arrange
            var builder = new PresidentBuilder(LocalNotification)
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress("Rua de teste", "123", "APT 12", "99380000");

            // Act
            builder.Build();

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Create_President_With_Invalid_Name()
        {
            // Arrange
            var builder = new PresidentBuilder(LocalNotification)
                  .WithId("1")
                  .WithName(null)
                  .WithAddress("Rua de teste", "123", "APT 12", "99380000");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_President_With_Invalid_ZipCode()
        {
            // Arrange
            var builder = new PresidentBuilder(LocalNotification)
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress("Rua de teste", "123", "APT 12", "993800021120");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_President_With_Invalid_Address()
        {
            // Arrange
            var builder = new PresidentBuilder(LocalNotification)
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress(null, "123", "APT 12", "99380000");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_President_With_Invalid_Address_Number()
        {
            // Arrange
            var builder = new PresidentBuilder(LocalNotification)
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress("Rua de teste", null, "APT 12", "99380000");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressNumberMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_President_With_Invalid_Address_Complement()
        {
            // Arrange
            var builder = new PresidentBuilder(LocalNotification)
                  .WithId("1")
                  .WithName("George Washington")
                  .WithAddress("Rua de teste", "123", null, "99380000");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
        }
    }
}
