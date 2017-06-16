using Tnf.App.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore;
using Xunit;
using Shouldly;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.Enumerables;
using System.Linq;
using Tnf.Architecture.Domain.Registration;
using Tnf.App.Dto.Response;
using Tnf.App.Dto.Request;
using Tnf.App.Bus.Notifications;

namespace Tnf.Architecture.Application.Tests.Services
{
    public class SpecialtyAppServiceTests : TnfEfCoreIntegratedTestBase<EfCoreAppTestModule>
    {
        private readonly ISpecialtyAppService _specialtyAppService;
        private readonly SpecialtyPoco _specialtyPoco;

        public SpecialtyAppServiceTests()
        {
            _specialtyAppService = Resolve<ISpecialtyAppService>();

            _specialtyPoco = new SpecialtyPoco()
            {
                Id = 1,
                Description = "Anestesiologia"
            };

            // Setup
            UsingDbContext<LegacyDbContext>(context => context.Specialties.Add(_specialtyPoco));
        }

        [Fact]
        public void Should_Get_All_Specialties_With_Success()
        {
            //Act
            var response = _specialtyAppService.GetAllSpecialties(new GetAllSpecialtiesDto() { PageSize = 10 });

            //Assert
            Assert.False(LocalNotification.HasNotification());
            response.Items.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Get_All_Specialties_With_Error()
        {
            //Act
            var response = _specialtyAppService.GetAllSpecialties(new GetAllSpecialtiesDto());

            //Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public void Should_Insert_Specialty_With_Success()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 2,
                Description = "Cirurgia Geral"
            };

            //Act
            var result = _specialtyAppService.CreateSpecialty(specialtyDto);

            //Assert
            Assert.False(LocalNotification.HasNotification());
            result.Id.ShouldBe(2);
        }

        [Fact]
        public void Should_Insert_Specialty_With_Error()
        {
            // Act
            var response = _specialtyAppService.CreateSpecialty(new SpecialtyDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public void Should_Insert_Null_Specialty_With_Error()
        {
            // Act
            var response = _specialtyAppService.CreateSpecialty(null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public void Should_Update_Specialty_With_Success()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 2,
                Description = "Cirurgia Geral"
            };

            //Act
            var result = _specialtyAppService.CreateSpecialty(specialtyDto);

            //Assert
            Assert.False(LocalNotification.HasNotification());

            result.Id.ShouldBe(2);

            result.Description = "Cirurgia Vascular";

            result = _specialtyAppService.UpdateSpecialty(result.Id, result);

            //Assert
            Assert.False(LocalNotification.HasNotification());
            result.Description.ShouldBe("Cirurgia Vascular");
        }

        [Fact]
        public void Should_Update_Specialty_With_Error()
        {
            // Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 99,
                Description = "Cirurgia Geral"
            };

            //Act
            var response = _specialtyAppService.UpdateSpecialty(specialtyDto.Id, specialtyDto);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public void Should_Update_Invalid_Id_With_Error()
        {
            // Act
            var response = _specialtyAppService.UpdateSpecialty(0, new SpecialtyDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public void Should_Update_Null_Specialty_With_Error()
        {
            // Act
            var response = _specialtyAppService.UpdateSpecialty(1, null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public void Should_Get_Specialty_With_Success()
        {
            //Act
            var response = _specialtyAppService.GetSpecialty(new RequestDto<int>(1));

            //Assert
            Assert.False(LocalNotification.HasNotification());
            response.Id.ShouldBe(1);
            response.Description.ShouldBe(_specialtyPoco.Description);
        }

        [Fact]
        public void Should_Get_Specialty_With_Error()
        {
            // Act
            var response = _specialtyAppService.GetSpecialty(new RequestDto<int>(99));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public void Should_Delete_Specialty_With_Success()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 2,
                Description = "Cirurgia Geral"
            };

            //Act
            var result = _specialtyAppService.CreateSpecialty(specialtyDto);

            //Assert
            Assert.False(LocalNotification.HasNotification());
            result.Id.ShouldBe(2);

            //Act
            _specialtyAppService.DeleteSpecialty(2);

            //Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Should_Delete_Specialty_With_Error()
        {
            // Act
            _specialtyAppService.DeleteSpecialty(99);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }
    }
}
