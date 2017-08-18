using Tnf.Architecture.Domain.Registration;
using Xunit;
using System.Linq;
using Tnf.App.TestBase;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class PersonBuilderTests : TnfAppIntegratedTestBase<DomainModuleTest>
    {
        [Fact]
        public void Create_Valid_Person()
        {
            // Arrange
            var builder = new PersonBuilder(LocalNotification)
                  .WithName("John");

            // Act
            builder.Build();

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Create_Person_With_Invalid_Description()
        {
            // Arrange
            var builder = new PersonBuilder(LocalNotification)
                  .WithName(null);

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Person.Error.PersonNameMustHaveValue.ToString()));
        }
    }
}
