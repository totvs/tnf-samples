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


            _profissionalRepository.GetAllProfessionals(Arg.Any<GetAllProfessionalsDto>())
                .Returns(professionalPaging);

            _profissionalRepository.GetProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(professionalDto);

            _profissionalRepository.CreateProfessional(Arg.Any<ProfessionalDto>())
                .Returns(professionalDto);

            _profissionalRepository.UpdateProfessional(Arg.Any<ProfessionalDto>())
                .Returns(professionalDto);

            _profissionalRepository.DeleteProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);

            _profissionalRepository.ExistsProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);


            _profissionalService = new ProfessionalService(_profissionalRepository);
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
            var requestDto = new GetAllProfessionalsDto()
            {
                PageSize = 2
            };

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
            var responseBase = _profissionalService.CreateProfessional(new ProfessionalDto()
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
            var responseBase = _profissionalService.CreateProfessional(new ProfessionalDto());

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

    }
}
