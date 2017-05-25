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
using Tnf.Dto.Response;
using Tnf.Dto.Request;

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

            var specialtyPaging = new SuccessResponseListDto<SpecialtyDto>();
            specialtyPaging.Items = specialtyList;

            var builder = new SpecialtyBuilder()
                .WithId(specialtyDto.Id)
                .WithDescription(specialtyDto.Description);

            _specialtyRepository.GetAllSpecialties(Arg.Any<GetAllSpecialtiesDto>())
                .Returns(specialtyPaging);

            _specialtyRepository.GetSpecialty(Arg.Any<RequestDto<int>>())
                .Returns(specialtyDto);

            _specialtyRepository.CreateSpecialty(Arg.Any<Specialty>())
                .Returns(1);

            _specialtyRepository.UpdateSpecialty(Arg.Any<Specialty>())
                .Returns(builder.Instance);

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
            Assert.True(allSpecialties.Items.Count == 1);
        }

        [Fact]
        public void Specialty_Service_Return_Specialty()
        {
            // Act
            var response = _specialtyService.GetSpecialty(new RequestDto<int>(1));

            // Assert
            Assert.True(response.Success);
            Assert.IsType(typeof(SpecialtyDto), response);
            var successResponse = response as SpecialtyDto;
            Assert.True(successResponse.Id == 1);
            Assert.True(successResponse.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Not_Return_Non_Existing_Specialty()
        {
            // Act
            var response = _specialtyService.GetSpecialty(new RequestDto<int>(99));

            // Assert
            Assert.False(response.Success);
            Assert.IsType(typeof(ErrorResponseDto), response);
            var errorResponse = response as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public void Specialty_Service_Delete_Specialty()
        {
            // Act
            var response = _specialtyService.DeleteSpecialty(1);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public void Specialty_Service_Delete_Not_Accept_Non_Existing_Specialty()
        {
            // Act
            var response = _specialtyService.DeleteSpecialty(99);

            // Assert
            Assert.False(response.Success);
            Assert.IsType(typeof(ErrorResponseDto), response);
            var errorResponse = response as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
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
            Assert.IsType(typeof(SpecialtyDto), responseBase);
            var specialty = responseBase as SpecialtyDto;
            Assert.True(specialty.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Insert_Not_Accept_Invalid_Specialty()
        {
            // Act
            var responseBase = _specialtyService.CreateSpecialty(new SpecialtyDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.IsType(typeof(ErrorResponseDto), responseBase);
            var errorResponse = responseBase as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
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
            Assert.IsType(typeof(SpecialtyDto), responseBase);
            var specialty = responseBase as SpecialtyDto;
            Assert.True(specialty.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Update_Not_Accept_Invalid_Specialty()
        {
            // Act
            var responseBase = _specialtyService.UpdateSpecialty(new SpecialtyDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.IsType(typeof(ErrorResponseDto), responseBase);
            var errorResponse = responseBase as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
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
            Assert.IsType(typeof(ErrorResponseDto), responseBase);
            var errorResponse = responseBase as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

    }
}
