using Shouldly;
using System;
using System.Linq;
using Tnf.App.Dto.Request;
using Tnf.App.TestBase;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.Registration;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class ProfessionalServiceTests : TnfAppIntegratedTestBase<DomainModuleTest>
    {
        private readonly IProfessionalService _profissionalService;

        public ProfessionalServiceTests()
        {
            _profissionalService = Resolve<IProfessionalService>();
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _profissionalService.ShouldNotBeNull();
        }

        [Fact]
        public void Professional_Service_Return_Professional()
        {
            // Act
            var response = _profissionalService.GetProfessional(new RequestDto<ComposeKey<Guid, decimal>>(new ComposeKey<Guid, decimal>(Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"), 1)));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(response.ProfessionalId == 1);
            Assert.True(response.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"));
            Assert.True(response.Name == "João da Silva");
        }

        [Fact]
        public void Professional_Service_Not_Return_Non_Existing_Professional()
        {
            // Act
            _profissionalService.GetProfessional(new RequestDto<ComposeKey<Guid, decimal>>(new ComposeKey<Guid, decimal>(Guid.NewGuid(), 99)));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            Assert.True(LocalNotification.GetAll().Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public void Professional_Service_Delete_Professional()
        {
            // Act
            _profissionalService.DeleteProfessional(new ComposeKey<Guid, decimal>(Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"), 1));

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Professional_Service_Delete_Not_Accept_Non_Existing_Professional()
        {
            // Act
            _profissionalService.DeleteProfessional(new ComposeKey<Guid, decimal>(Guid.NewGuid(), 99));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public void Professional_Service_Insert_Valid_Professional()
        {
            // Arrange
            var professionalBuilder = new ProfessionalBuilder(LocalNotification)
                .WithName("João da Silva")
                .WithPhone("15398264438")
                .WithEmail("email2@email2.com")
                .WithAddress(new Address("Rua do comercio 2", "1233", "APT 1234", new ZipCode("22888888")));

            // Act
            var responseBase = _profissionalService.CreateProfessional(professionalBuilder);

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.NotEqual(responseBase.PrimaryKey, Guid.Empty);
            Assert.NotEqual(responseBase.SecundaryKey, 0);
        }

        [Fact]
        public void Professional_Service_Insert_Not_Accept_Invalid_Professional()
        {
            // Act
            _profissionalService.CreateProfessional(new ProfessionalBuilder(LocalNotification));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Professional_Service_Update_Valid_Professional()
        {
            // Arrange
            var professionalBuilder = new ProfessionalBuilder(LocalNotification)
                .WithProfessionalId(1)
                .WithCode(Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"))
                .WithName("João da Silva")
                .WithPhone("15398264438")
                .WithEmail("email2@email2.com")
                .WithAddress(new Address("Rua do comercio 2", "1233", "APT 1234", new ZipCode("22888888")));

            // Act
            _profissionalService.UpdateProfessional(professionalBuilder);

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Professional_Service_Update_Not_Accept_Invalid_Professional()
        {
            // Act
            _profissionalService.UpdateProfessional(new ProfessionalBuilder(LocalNotification));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Professional_Service_Update_Not_Accept_Non_Existing_Professional()
        {
            // Arrange
            var professionalBuilder = new ProfessionalBuilder(LocalNotification)
                .WithProfessionalId(99)
                .WithCode(Guid.NewGuid())
                .WithName("João da Silva")
                .WithPhone("15398264438")
                .WithEmail("email2@email2.com")
                .WithAddress(new Address("Rua do comercio 2", "1233", "APT 1234", new ZipCode("22888888")));

            // Act
            _profissionalService.UpdateProfessional(professionalBuilder);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

    }
}
