using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.EntityFrameworkCore;
using Xunit;
using Shouldly;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.Registration;
using System.Collections.Generic;

namespace Tnf.Architecture.Application.Tests.Registration
{
    public class ProfessionalAppServiceTests : TnfEfCoreIntegratedTestBase<EfCoreAppTestModule>
    {
        private readonly IProfessionalAppService _professionalAppService;
        private readonly ProfessionalPoco _professionalPoco;
        private readonly SpecialtyPoco _specialtyPoco;

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
            UsingDbContext<LegacyDbContext>(context => context.Professionals.Add(_professionalPoco));

            _specialtyPoco = new SpecialtyPoco()
            {
                Id = 1,
                Description = "Anestesiologia"
            };

            // Setup
            UsingDbContext<LegacyDbContext>(context => context.Specialties.Add(_specialtyPoco));
        }

        [Fact]
        public void Professional_Repository_Should_Be_All()
        {
            //Act
            var count = _professionalAppService.GetAllProfessionals(new GetAllProfessionalsDto());

            //Assert
            count.Total.ShouldBe(1);
        }

        [Fact]
        public void Professional_Repository_Should_Be_Insert_And_Update_Item()
        {
            var professionalDto = new ProfessionalCreateDto()
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

            result.Data.Name = "Rua alterada de teste";

            result.Data.Specialties.Clear();
            result = _professionalAppService.UpdateProfessional(result.Data);

            //Assert
            result.Success.ShouldBeTrue();
            result.Data.Name.ShouldBe("Rua alterada de teste");

            var professional = _professionalAppService.GetProfessional(new ProfessionalKeysDto(result.Data.ProfessionalId, result.Data.Code));

            //Assert
            professional.Specialties.ShouldBeEmpty();
        }

        [Fact]
        public void Professional_Repository_Should_Get_Item()
        {
            //Act
            var result = _professionalAppService.GetProfessional(new ProfessionalKeysDto(1, _professionalPoco.Code));

            //Assert
            result.ProfessionalId.ShouldBe(1);
            result.Code.ShouldBe(_professionalPoco.Code);
        }

        [Fact]
        public void Professional_Repository_Should_Delete_Item()
        {
            //Act
            _professionalAppService.DeleteProfessional(new ProfessionalKeysDto(1, _professionalPoco.Code));

            var count = _professionalAppService.GetAllProfessionals(new GetAllProfessionalsDto());

            //Assert
            count.Data.ShouldBeEmpty();
        }

        [Fact]
        public void Specialities_Repository_Should_Be_All()
        {
            //Act
            var count = _professionalAppService.GetAllSpecialties(new GetAllSpecialtiesDto());

            //Assert
            count.Total.ShouldBe(1);
        }

        [Fact]
        public void Specialities_Repository_Should_Be_Insert_And_Update_Item()
        {
            var specialtyDto = new SpecialtyDto()
            {
                Id = 2,
                Description = "Cirurgia Geral"
            };

            //Act
            var result = _professionalAppService.CreateSpecialty(specialtyDto);

            //Assert
            result.Success.ShouldBeTrue();
            result.Data.Id.ShouldBe(2);

            result.Data.Description = "Cirurgia Vascular";

            result = _professionalAppService.UpdateSpecialty(result.Data);

            //Assert
            result.Success.ShouldBeTrue();
            result.Data.Description.ShouldBe("Cirurgia Vascular");
        }

        [Fact]
        public void Specialities_Repository_Should_Get_Item()
        {
            //Act
            var result = _professionalAppService.GetSpecialty(1);

            //Assert
            result.Id.ShouldBe(1);
            result.Description.ShouldBe(_specialtyPoco.Description);
        }

        [Fact]
        public void Specialities_Repository_Should_Delete_Item()
        {
            //Act
            _professionalAppService.DeleteSpecialty(1);

            var count = _professionalAppService.GetAllSpecialties(new GetAllSpecialtiesDto());

            //Assert
            count.Data.ShouldBeEmpty();
        }
    }
}
