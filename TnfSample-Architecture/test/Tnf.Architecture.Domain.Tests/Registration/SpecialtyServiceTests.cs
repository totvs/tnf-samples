using NSubstitute;
using System.Collections.Generic;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Domain.Registration;
using Xunit;
using Shouldly;
using System.Linq;
using Tnf.App.Dto.Response;
using Tnf.App.Dto.Request;
using Tnf.App.Bus.Notifications;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class SpecialtyServiceTests : TestBaseWithLocalIocManager
    {
        ISpecialtyService _specialtyService;

        public SpecialtyServiceTests()
        {
            var _specialtyRepository = Substitute.For<ISpecialtyRepository>();

            var specialtyDto = new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Vascular"
            };

            var specialtyList = new List<SpecialtyDto>() { specialtyDto };

            var specialtyPaging = new ListDto<SpecialtyDto>();
            specialtyPaging.Items = specialtyList;

            var specialty = new Specialty()
            {
                Id = specialtyDto.Id,
                Description = specialtyDto.Description
            };

            _specialtyRepository.GetAllSpecialties(Arg.Any<GetAllSpecialtiesDto>())
                .Returns(specialtyPaging);

            _specialtyRepository.GetSpecialty(Arg.Any<RequestDto<int>>())
                .Returns(specialtyDto);

            _specialtyRepository.CreateSpecialty(Arg.Any<Specialty>())
                .Returns(1);

            _specialtyRepository.UpdateSpecialty(Arg.Any<Specialty>())
                .Returns(specialty);

            _specialtyRepository.DeleteSpecialty(Arg.Any<int>());

            _specialtyRepository.ExistsSpecialty(Arg.Is(1))
                .Returns(true);

            _specialtyService = new SpecialtyService(_specialtyRepository);
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _specialtyService.ShouldNotBeNull();
        }

        [Fact]
        public void Specialty_Service_Return_All_Values()
        {
            // Arrange
            var requestDto = new GetAllSpecialtiesDto()
            {
                PageSize = 2
            };

            // Act
            var allSpecialties = _specialtyService.GetAllSpecialties(requestDto);

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(allSpecialties.Items.Count == 1);
        }

        [Fact]
        public void Specialty_Service_Return_Specialty()
        {
            // Act
            var response = _specialtyService.GetSpecialty(new RequestDto<int>(1));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(response.Id == 1);
            Assert.True(response.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Not_Return_Non_Existing_Specialty()
        {
            // Act
            var response = _specialtyService.GetSpecialty(new RequestDto<int>(99));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public void Specialty_Service_Delete_Specialty()
        {
            // Act
            _specialtyService.DeleteSpecialty(1);

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Specialty_Service_Delete_Not_Accept_Non_Existing_Specialty()
        {
            // Act
            _specialtyService.DeleteSpecialty(99);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public void Specialty_Service_Insert_Valid_Specialty()
        {
            // Act
            var responseBase = _specialtyService.CreateSpecialty(new SpecialtyDto()
            {
                Description = "Cirurgia Vascular"
            });

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(responseBase.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Insert_Not_Accept_Invalid_Specialty()
        {
            // Act
            var responseBase = _specialtyService.CreateSpecialty(new SpecialtyDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public void Specialty_Service_Update_Valid_Specialty()
        {
            // Act
            var responseBase = _specialtyService.UpdateSpecialty(new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Vascular"
            });

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(responseBase.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Update_Not_Accept_Invalid_Specialty()
        {
            // Act
            var responseBase = _specialtyService.UpdateSpecialty(new SpecialtyDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public void Specialty_Service_Update_Not_Accept_Non_Existing_Specialty()
        {
            // Act
            var responseBase = _specialtyService.UpdateSpecialty(new SpecialtyDto()
            {
                Id = 99,
                Description = "Cirurgia Vascular"
            });

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

    }
}
