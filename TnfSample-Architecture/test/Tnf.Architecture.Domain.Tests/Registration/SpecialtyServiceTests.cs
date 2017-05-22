using NSubstitute;
using System.Collections.Generic;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using System;
using Tnf.Architecture.Domain.Registration;
using Xunit;
using Shouldly;
using System.Linq;

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

            var specialtyPaging = new PagingResponseDto<SpecialtyDto>(specialtyList);

            _specialtyRepository.GetAllSpecialties(Arg.Any<GetAllSpecialtiesDto>())
                .Returns(specialtyPaging);

            _specialtyRepository.GetSpecialty(Arg.Any<int>())
                .Returns(specialtyDto);

            _specialtyRepository.CreateSpecialty(Arg.Any<SpecialtyDto>())
                .Returns(specialtyDto);

            _specialtyRepository.UpdateSpecialty(Arg.Any<SpecialtyDto>())
                .Returns(specialtyDto);

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
            Assert.True(allSpecialties.Success);
            Assert.Empty(allSpecialties.Notifications);
            Assert.True(allSpecialties.Data.Count == 1);
        }

        [Fact]
        public void Specialty_Service_Return_Specialty()
        {
            // Act
            var specialty = _specialtyService.GetSpecialty(1);

            // Assert
            Assert.True(specialty.Id == 1);
            Assert.True(specialty.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Not_Return_Non_Existing_Specialty()
        {
            // Act
            var specialty = _specialtyService.GetSpecialty(99);

            // Assert
            Assert.Null(specialty);
        }

        [Fact]
        public void Specialty_Service_Delete_Specialty()
        {
            // Act
            var response = _specialtyService.DeleteSpecialty(1);

            // Assert
            Assert.True(response.Success);
            Assert.Empty(response.Notifications);
        }

        [Fact]
        public void Specialty_Service_Delete_Not_Accept_Non_Existing_Specialty()
        {
            // Act
            var response = _specialtyService.DeleteSpecialty(99);

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
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
            Assert.True(responseBase.Success);
            Assert.Empty(responseBase.Notifications);
            Assert.True(responseBase.Data.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Insert_Not_Accept_Invalid_Specialty()
        {
            // Act
            var responseBase = _specialtyService.CreateSpecialty(new SpecialtyDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.Null(responseBase.Data);
            Assert.True(responseBase.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
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
            Assert.True(responseBase.Success);
            Assert.Empty(responseBase.Notifications);
            Assert.True(responseBase.Data.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Update_Not_Accept_Invalid_Specialty()
        {
            // Act
            var responseBase = _specialtyService.UpdateSpecialty(new SpecialtyDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.True(responseBase.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
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
            Assert.False(responseBase.Success);
            Assert.Null(responseBase.Data);
            Assert.True(responseBase.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

    }
}
