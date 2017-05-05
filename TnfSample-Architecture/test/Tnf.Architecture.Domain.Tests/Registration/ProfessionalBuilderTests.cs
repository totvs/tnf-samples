using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Xunit;
using System.Linq;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class ProfessionalBuilderTests
    {
        [Fact]
        public void Create_Valid_Professional()
        {
            // Arrange
            var builder = new ProfessionalBuilder()
                  .WithName("Professional 1")
                  .WithAddress("Rua do comercio", "123", "APT 1", new ZipCode("12345678"))
                  .WithEmail("professional@professional.com")
                  .WithPhone("5563524178");

            // Act
            var build = builder.Build();

            // Assert
            Assert.True(build.Success);
        }

        [Fact]
        public void Create_Invalid_Professional()
        {
            // Arrange
            var builder = new ProfessionalBuilder();

            // Act
            var build = builder.Build();

            // Assert
            Assert.False(build.Success);
            Assert.True(build.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(build.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(build.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(build.Notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(build.Notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(build.Notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(build.Notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }
    }
}
