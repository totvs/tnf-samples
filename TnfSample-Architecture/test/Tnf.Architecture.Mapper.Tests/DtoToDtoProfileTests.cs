using Xunit;
using Tnf.AutoMapper;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using System.Collections.Generic;
using Tnf.TestBase;

namespace Tnf.Architecture.Mapper.Tests
{
    public class DtoToDtoProfileTests : TnfIntegratedTestBase<MapperTestModule>
    {
        [Fact]
        public void MapTo_ProfessionalUpdateDto_To_ProfessionalDto()
        {
            ProfessionalUpdateDto dto = new ProfessionalUpdateDto()
            {
                Address = new Address("Rua de teste", "123", "APT 123", new ZipCode("12345678")),
                Email = "email@email.com",
                Name = "Nome do professional",
                Phone = "998765412",
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description= "Clinica Geral" }
                }
            };

            ProfessionalDto mappDto = dto.MapTo<ProfessionalDto>();

            Assert.NotNull(dto.Address);
            Assert.Equal(dto.Address.Complement, mappDto.Address.Complement);
            Assert.Equal(dto.Address.Number, mappDto.Address.Number);
            Assert.Equal(dto.Address.Street, mappDto.Address.Street);
            Assert.Equal(dto.Address.ZipCode.Number, mappDto.Address.ZipCode.Number);
        }
    }
}
