using System;
using System.Collections.Generic;
using Tnf.Architecture.Dto;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Xunit;
using Tnf.AutoMapper;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Data.Entities;
using Tnf.TestBase;

namespace Tnf.Architecture.Mapper.Tests
{
    public class DtoToPocoProfileTests : TnfIntegratedTestBase<MapperTestModule>
    {
        [Fact]
        public void MapTo_CountryDto_To_CountryPoco()
        {
            CountryDto dto = new CountryDto()
            {
                Id = 1,
                Name = "Brasil"
            };

            CountryPoco mappPoco = dto.MapTo<CountryPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(mappPoco.Id, mappPoco.Id);
            Assert.Equal(mappPoco.Name, mappPoco.Name);
        }

        [Fact]
        public void MapTo_ProfessionalDto_To_ProfessionalPoco()
        {
            ProfessionalDto dto = new ProfessionalDto()
            {
                Address = new Address("Rua de teste", "123", "Compelemento", new ZipCode("12345678")),
                Code = Guid.NewGuid(),
                Email = "email@email.com",
                Name = "Professional teste",
                Phone = "988765541",
                ProfessionalId = 1,
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description = "Especialidade" }
                }
            };

            ProfessionalPoco mappPoco = dto.MapTo<ProfessionalPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(dto.Name, mappPoco.Name);
            Assert.Equal(dto.Email, mappPoco.Email);
            Assert.Equal(dto.Phone, mappPoco.Phone);
            Assert.NotNull(mappPoco.Address);
            Assert.Equal(dto.Address.Complement, mappPoco.AddressComplement);
            Assert.Equal(dto.Address.Number, mappPoco.AddressNumber);
            Assert.Equal(dto.Address.Street, mappPoco.Address);
            Assert.Equal(dto.Address.ZipCode.Number, mappPoco.ZipCode);
            Assert.Null(mappPoco.ProfessionalSpecialties);
        }

        [Fact]
        public void MapTo_SpecialtyDto_To_SpecialtyPoco()
        {
            SpecialtyDto dto = new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Geral"
            };

            SpecialtyPoco mappPoco = dto.MapTo<SpecialtyPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(mappPoco.Id, mappPoco.Id);
            Assert.Equal(mappPoco.Description, mappPoco.Description);
        }

        [Fact]
        public void MapTo_PresidentDto_To_PresidentPoco()
        {
            PresidentDto dto = new PresidentDto()
            {
                Id = "1234",
                Name = "George",
                Address = new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))
            };

            PresidentPoco mappPoco = dto.MapTo<PresidentPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(dto.Id, mappPoco.Id);
            Assert.Equal(dto.Name, mappPoco.Name);
            Assert.NotNull(mappPoco.Address);
            Assert.Equal(dto.Address.Complement, mappPoco.Address.Complement);
            Assert.Equal(dto.Address.Number, mappPoco.Address.Number);
            Assert.Equal(dto.Address.Street, mappPoco.Address.Street);
            Assert.Equal(dto.Address.ZipCode.Number, mappPoco.Address.ZipCode.Number);
        }
    }
}
