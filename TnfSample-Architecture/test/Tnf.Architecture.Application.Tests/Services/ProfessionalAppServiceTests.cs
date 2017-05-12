using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.EntityFrameworkCore;
using Xunit;
using Shouldly;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.Registration;
using System.Collections.Generic;
using System.Linq;
using Tnf.Architecture.Domain.Registration;

namespace Tnf.Architecture.Application.Tests.Services
{
    public class ProfessionalAppServiceTests : TnfEfCoreIntegratedTestBase<EfCoreAppTestModule>
    {
        private readonly IProfessionalAppService _professionalAppService;
        private readonly ProfessionalPoco _professionalPoco;

        public ProfessionalAppServiceTests()
        {
            _professionalAppService = Resolve<IProfessionalAppService>();

            _professionalPoco = new ProfessionalPoco()
            {
                ProfessionalId = 1,
                Address = "Rua do comercio",
                AddressNumber = "123",
                AddressComplement = "APT 123",
                Email = "email@email.com",
                Name = "João da Silva",
                Phone = "55998765432",
                ZipCode = "99888777"
            };

            // Setup
            UsingDbContext<LegacyDbContext>(context =>
            {
                context.Professionals.Add(_professionalPoco);
                context.Specialties.Add(new SpecialtyPoco() { Id = 1, Description = "Anestesiologia" });                
            });
        }

        [Fact]
        public void Should_Get_All_Professionals_With_Success()
        {
            //Act
            var count = _professionalAppService.GetAllProfessionals(new GetAllProfessionalsDto());

            //Assert
            count.Total.ShouldBe(1);
        }

        [Fact]
        public void Should_Insert_Professional_With_Success()
        {
            //Arrange
            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 2,
                Address = new Address("Rua teste", "98765", "APT 9876", new ZipCode("23156478")),
                Email = "email1234@email.com",
                Name = "Jose da Silva",
                Phone = "58962348",
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description = "Anestesiologia" }
                }
            };

            //Act
            var result = _professionalAppService.CreateProfessional(professionalDto);

            //Assert
            result.Success.ShouldBeTrue();
            result.Data.ProfessionalId.ShouldBe(2);
        }

        [Fact]
        public void Should_Insert_Professional_With_Error()
        {
            // Act
            var response = _professionalAppService.CreateProfessional(new ProfessionalDto());

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Should_Update_Professional_With_Success()
        {
            //Arrange
            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 2,
                Address = new Address("Rua teste", "98765", "APT 9876", new ZipCode("23156478")),
                Email = "email1234@email.com",
                Name = "Jose da Silva",
                Phone = "58962348",
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description = "Anestesiologia" }
                }
            };

            //Act
            var result = _professionalAppService.CreateProfessional(professionalDto);

            //Assert
            result.Success.ShouldBeTrue();
            result.Data.ProfessionalId.ShouldBe(2);

            result.Data.Name = "Nome Alterado Teste";

            result.Data.Specialties.Clear();
            result = _professionalAppService.UpdateProfessional(result.Data);

            //Assert
            result.Success.ShouldBeTrue();
            result.Data.Name.ShouldBe("Nome Alterado Teste");

            var professional = _professionalAppService.GetProfessional(new ProfessionalKeysDto(result.Data.ProfessionalId, result.Data.Code));

            //Assert
            professional.Specialties.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Update_Professional_With_Error()
        {
            // Arrange
            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 99,
                Address = new Address("Rua teste", "98765", "APT 9876", new ZipCode("23156478")),
                Email = "email1234@email.com",
                Name = "Jose da Silva",
                Phone = "58962348",
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description = "Anestesiologia" }
                }
            };

            //Act
            var response = _professionalAppService.UpdateProfessional(professionalDto);

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public void Should_Get_Professional_With_Success()
        {
            //Act
            var result = _professionalAppService.GetProfessional(new ProfessionalKeysDto(1, _professionalPoco.Code));

            //Assert
            result.ProfessionalId.ShouldBe(1);
            result.Code.ShouldBe(_professionalPoco.Code);
        }

        [Fact]
        public void Should_Get_Professional_With_Error()
        {
            // Act
            var result = _professionalAppService.GetProfessional(new ProfessionalKeysDto(99, _professionalPoco.Code));

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Delete_Professional_With_Success()
        {
            //Act
            var response = _professionalAppService.DeleteProfessional(new ProfessionalKeysDto(1, _professionalPoco.Code));

            var count = _professionalAppService.GetAllProfessionals(new GetAllProfessionalsDto());

            //Assert
            Assert.True(response.Success);
            count.Data.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Delete_Professional_With_Error()
        {
            // Act
            var response = _professionalAppService.DeleteProfessional(new ProfessionalKeysDto(99, _professionalPoco.Code));

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }
    }
}
