using System.Linq;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class ProfessionalBuilderTests : TnfAppIntegratedTestBase<DomainModuleTest>
    {
        [Fact]
        public void Create_Valid_Professional()
        {
            // Arrange
            var builder = new ProfessionalBuilder(LocalNotification)
                  .WithName("Professional 1")
                  .WithAddress("Rua do comercio", "123", "APT 1", new ZipCode("12345678"))
                  .WithEmail("professional@professional.com")
                  .WithPhone("5563524178");

            // Act
            builder.Build();

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Create_Professional_With_Invalid_Name()
        {
            // Arrange
            var builder = new ProfessionalBuilder(LocalNotification)
                  .WithName(null)
                  .WithAddress("Rua do comercio", "123", "APT 1", new ZipCode("12345678"))
                  .WithEmail("professional@professional.com")
                  .WithPhone("5563524178");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_Professional_With_Invalid_ZipCode()
        {
            // Arrange
            var builder = new ProfessionalBuilder(LocalNotification)
                  .WithName("Professional 1")
                  .WithAddress("Rua do comercio", "123", "APT 1", new ZipCode(null))
                  .WithEmail("professional@professional.com")
                  .WithPhone("5563524178");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_Professional_With_Invalid_Address()
        {
            // Arrange
            var builder = new ProfessionalBuilder(LocalNotification)
                  .WithName("Professional 1")
                  .WithAddress(null, "123", "APT 1", new ZipCode("12345678"))
                  .WithEmail("professional@professional.com")
                  .WithPhone("5563524178");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_Professional_With_Invalid_Address_Number()
        {
            // Arrange
            var builder = new ProfessionalBuilder(LocalNotification)
                  .WithName("Professional 1")
                  .WithAddress("Rua do comercio", null, "APT 1", new ZipCode("12345678"))
                  .WithEmail("professional@professional.com")
                  .WithPhone("5563524178");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_Professional_With_Invalid_Address_Complement()
        {
            // Arrange
            var builder = new ProfessionalBuilder(LocalNotification)
                  .WithName("Professional 1")
                  .WithAddress("Rua do comercio", "123", null, new ZipCode("12345678"))
                  .WithEmail("professional@professional.com")
                  .WithPhone("5563524178");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_Professional_With_Invalid_Phone()
        {
            // Arrange
            var builder = new ProfessionalBuilder(LocalNotification)
                  .WithName("Professional 1")
                  .WithAddress("Rua do comercio", "123", "APT 1", new ZipCode("12345678"))
                  .WithEmail("professional@professional.com")
                  .WithPhone(null);

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
        }

        [Fact]
        public void Create_Professional_With_Invalid_Email()
        {
            // Arrange
            var builder = new ProfessionalBuilder(LocalNotification)
                  .WithName("Professional 1")
                  .WithAddress("Rua do comercio", "123", "APT 1", new ZipCode("12345678"))
                  .WithEmail(null)
                  .WithPhone("5563524178");

            // Act
            builder.Build();

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
        }
    }
}
