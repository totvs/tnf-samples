using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.EntityFrameworkCore;
using Xunit;
using Shouldly;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.Dto.Paging;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Tests.Registration
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
            UsingDbContext<LegacyDbContext>(context => context.Professionals.Add(_professionalPoco));
        }

        [Fact]
        public void Professional_Repository_Should_Be_All()
        {
            //Act
            var count = _professionalAppService.All(new GetAllProfessionalsDto());

            //Assert
            count.Total.ShouldBe(1);
        }

        [Fact]
        public void Professional_Repository_Should_Be_Insert_And_Update_Item()
        {
            var professionalDto = new ProfessionalCreateDto()
            {
                ProfessionalId = 2,
                Address = "Rua teste",
                AddressNumber = "98765",
                AddressComplement = "APT 9876",
                Email = "email1234@email.com",
                Name = "Jose da Silva",
                Phone = "58962348",
                ZipCode = new ZipCode("23156478")
            };

            //Act
            var result = _professionalAppService.Create(professionalDto);

            //Assert
            result.Success.ShouldBeTrue();
            result.Data.ProfessionalId.ShouldBe(2);

            result.Data.Name = "Rua alterada de teste";

            result = _professionalAppService.Update(result.Data);

            //Assert
            result.Success.ShouldBeTrue();
            result.Data.Name.ShouldBe("Rua alterada de teste");
        }

        [Fact]
        public void Professional_Repository_Should_Get_Item()
        {
            //Act
            var result = _professionalAppService.Get(new ProfessionalKeysDto(1, _professionalPoco.Code));

            //Assert
            result.ProfessionalId.ShouldBe(1);
            result.Code.ShouldBe(_professionalPoco.Code);
        }

        [Fact]
        public void Professional_Repository_Should_Delete_Item()
        {
            //Act
            _professionalAppService.Delete(new ProfessionalKeysDto(1, _professionalPoco.Code));

            var count = _professionalAppService.All(new GetAllProfessionalsDto());

            //Assert
            count.Data.ShouldBeEmpty();
        }
    }
}
