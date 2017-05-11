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
    public class ProfessionalServiceTests : TestBaseWithLocalIocManager
    {
        IProfessionalService _profissionalService;

        public ProfessionalServiceTests()
        {
            var _profissionalRepository = Substitute.For<IProfessionalRepository>();
            var _specialtyRepository = Substitute.For<ISpecialtyRepository>();

            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 1,
                Name = "João da Silva",
                Phone = "99997654",
                Email = "email@email.com",
                Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                Address = new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321"))
            };

            var professionalList = new List<ProfessionalDto>() { professionalDto };

            var professionalPaging = new PagingResponseDto<ProfessionalDto>(professionalList);


            var specialtyDto = new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Vascular"
            };

            var specialtyList = new List<SpecialtyDto>() { specialtyDto };

            var specialtyPaging = new PagingResponseDto<SpecialtyDto>(specialtyList);


            _profissionalRepository.GetAllProfessionals(Arg.Any<GetAllProfessionalsDto>())
                .Returns(professionalPaging);

            _profissionalRepository.GetProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(professionalDto);

            _profissionalRepository.CreateProfessional(Arg.Any<ProfessionalCreateDto>())
                .Returns(professionalDto);

            _profissionalRepository.UpdateProfessional(Arg.Any<ProfessionalDto>())
                .Returns(professionalDto);

            _profissionalRepository.DeleteProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);

            _profissionalRepository.ExistsProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);


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


            _profissionalService = new ProfessionalService(_profissionalRepository, _specialtyRepository);
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _profissionalService.ShouldNotBeNull();
        }

        [Fact]
        public void Professional_Service_Return_All_Values()
        {
            // Arrange
            var requestDto = new GetAllProfessionalsDto(0, 2);

            // Act
            var allProfessionals = _profissionalService.GetAllProfessionals(requestDto);

            // Assert
            Assert.True(allProfessionals.Success);
            Assert.Empty(allProfessionals.Notifications);
            Assert.True(allProfessionals.Data.Count == 1);
        }

        [Fact]
        public void Professional_Service_Return_Professional()
        {
            // Act
            var professional = _profissionalService.GetProfessional(new ProfessionalKeysDto(1, Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")));

            // Assert
            Assert.True(professional.ProfessionalId == 1);
            Assert.True(professional.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"));
            Assert.True(professional.Name == "João da Silva");
        }

        [Fact]
        public void Professional_Service_Not_Return_Non_Existing_Professional()
        {
            // Act
            var professional = _profissionalService.GetProfessional(new ProfessionalKeysDto(99, Guid.NewGuid()));

            // Assert
            Assert.Null(professional);
        }

        [Fact]
        public void Professional_Service_Delete_Professional()
        {
            // Act
            var response = _profissionalService.DeleteProfessional(new ProfessionalKeysDto(1, Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")));

            // Assert
            Assert.True(response.Success);
            Assert.Empty(response.Notifications);
        }

        [Fact]
        public void Professional_Service_Delete_Not_Accept_Non_Existing_Professional()
        {
            // Act
            var response = _profissionalService.DeleteProfessional(new ProfessionalKeysDto(99, Guid.NewGuid()));

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public void Professional_Service_Insert_Valid_Professional()
        {
            // Act
            var responseBase = _profissionalService.CreateProfessional(new ProfessionalCreateDto()
            {
                Address = new Address("Rua do comercio 2", "1233", "APT 1234", new ZipCode("22888888")),
                Email = "email2@email2.com",
                Name = "João da Silva",
                Phone = "15398264438"
            });

            // Assert
            Assert.True(responseBase.Success);
            Assert.Empty(responseBase.Notifications);
            Assert.True(responseBase.Data.Name == "João da Silva");
        }

        [Fact]
        public void Professional_Service_Insert_Not_Accept_Invalid_Professional()
        {
            // Act
            var responseBase = _profissionalService.CreateProfessional(new ProfessionalCreateDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.Null(responseBase.Data);
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Professional_Service_Update_Valid_Professional()
        {
            // Act
            var responseBase = _profissionalService.UpdateProfessional(new ProfessionalDto()
            {
                ProfessionalId = 1,
                Name = "João da Silva",
                Phone = "99997654",
                Email = "email@email.com",
                Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                Address = new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321"))
            });

            // Assert
            Assert.True(responseBase.Success);
            Assert.Empty(responseBase.Notifications);
            Assert.True(responseBase.Data.Name == "João da Silva");
        }

        [Fact]
        public void Professional_Service_Update_Not_Accept_Invalid_Professional()
        {
            // Act
            var responseBase = _profissionalService.UpdateProfessional(new ProfessionalDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Professional_Service_Update_Not_Accept_Non_Existing_Professional()
        {
            // Act
            var responseBase = _profissionalService.UpdateProfessional(new ProfessionalDto()
            {
                ProfessionalId = 99,
                Name = "João da Silva",
                Phone = "99997654",
                Email = "email@email.com",
                Code = Guid.NewGuid(),
                Address = new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321"))
            });

            // Assert
            Assert.False(responseBase.Success);
            Assert.Null(responseBase.Data);
            Assert.True(responseBase.Notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }



        [Fact]
        public void Specialty_Service_Return_All_Values()
        {
            // Arrange
            var requestDto = new GetAllSpecialtiesDto(0, 2);

            // Act
            var allSpecialties = _profissionalService.GetAllSpecialties(requestDto);

            // Assert
            Assert.True(allSpecialties.Success);
            Assert.Empty(allSpecialties.Notifications);
            Assert.True(allSpecialties.Data.Count == 1);
        }

        [Fact]
        public void Specialty_Service_Return_Specialty()
        {
            // Act
            var specialty = _profissionalService.GetSpecialty(1);

            // Assert
            Assert.True(specialty.Id == 1);
            Assert.True(specialty.Description == "Cirurgia Vascular");
        }

        [Fact]
        public void Specialty_Service_Not_Return_Non_Existing_Specialty()
        {
            // Act
            var specialty = _profissionalService.GetSpecialty(99);

            // Assert
            Assert.Null(specialty);
        }

        [Fact]
        public void Specialty_Service_Delete_Specialty()
        {
            // Act
            var response = _profissionalService.DeleteSpecialty(1);

            // Assert
            Assert.True(response.Success);
            Assert.Empty(response.Notifications);
        }

        [Fact]
        public void Specialty_Service_Delete_Not_Accept_Non_Existing_Specialty()
        {
            // Act
            var response = _profissionalService.DeleteSpecialty(99);

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public void Specialty_Service_Insert_Valid_Specialty()
        {
            // Act
            var responseBase = _profissionalService.CreateSpecialty(new SpecialtyDto()
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
            var responseBase = _profissionalService.CreateSpecialty(new SpecialtyDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.Null(responseBase.Data);
            Assert.True(responseBase.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public void Specialty_Service_Update_Valid_Specialty()
        {
            // Act
            var responseBase = _profissionalService.UpdateSpecialty(new SpecialtyDto()
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
            var responseBase = _profissionalService.UpdateSpecialty(new SpecialtyDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.True(responseBase.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public void Specialty_Service_Update_Not_Accept_Non_Existing_Specialty()
        {
            // Act
            var responseBase = _profissionalService.UpdateSpecialty(new SpecialtyDto()
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
