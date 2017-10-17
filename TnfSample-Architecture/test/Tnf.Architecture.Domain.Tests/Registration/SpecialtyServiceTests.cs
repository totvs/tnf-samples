using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.Registration;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class SpecialtyServiceTests : TnfAppIntegratedTestBase<DomainModuleTest>
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtyServiceTests()
        {
            _specialtyService = Resolve<ISpecialtyService>();
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _specialtyService.ShouldNotBeNull();
        }

        [Fact]
        public async Task Specialty_Service_Return_Specialty()
        {
            // Act
            var response = await _specialtyService.GetSpecialty(new RequestDto(1));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(response.Id == 1);
            Assert.True(response.Description == "Cirurgia Vascular");
        }

        [Fact]
        public async Task Specialty_Service_Not_Return_Non_Existing_Specialty()
        {
            // Act
            await _specialtyService.GetSpecialty(new RequestDto(99));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public async Task Specialty_Service_Delete_Specialty()
        {
            // Act
            await _specialtyService.DeleteSpecialty(1);

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public async Task Specialty_Service_Delete_Not_Accept_Non_Existing_Specialty()
        {
            // Act
            await _specialtyService.DeleteSpecialty(99);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public async Task Specialty_Service_Insert_Valid_Specialty()
        {
            // Arrange
            var specialtyBuilder = new SpecialtyBuilder(LocalNotification)
                .WithDescription("Cirurgia Vascular");

            // Act
            var responseBase = await _specialtyService.CreateSpecialty(specialtyBuilder);

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.NotEqual(responseBase, 0);
        }

        [Fact]
        public async Task Specialty_Service_Insert_Not_Accept_Invalid_Specialty()
        {
            // Act
            await _specialtyService.CreateSpecialty(new SpecialtyBuilder(LocalNotification));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Specialty_Service_Update_Valid_Specialty()
        {
            // Arrange
            var specialtyBuilder = new SpecialtyBuilder(LocalNotification)
                .WithId(1)
                .WithDescription("Cirurgia Vascular");

            // Act
            await _specialtyService.UpdateSpecialty(specialtyBuilder);

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public async Task Specialty_Service_Update_Not_Accept_Invalid_Specialty()
        {
            // Act
            await _specialtyService.UpdateSpecialty(new SpecialtyBuilder(LocalNotification));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Specialty_Service_Update_Not_Accept_Non_Existing_Specialty()
        {
            // Arrange
            var specialtyBuilder = new SpecialtyBuilder(LocalNotification)
                .WithId(99)
                .WithDescription("Cirurgia Vascular");

            // Act
            await _specialtyService.UpdateSpecialty(specialtyBuilder);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

    }
}
