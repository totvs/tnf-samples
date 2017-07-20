using System;
using System.Collections.Generic;
using Tnf.App.TestBase;
using Tnf.Architecture.Carol.Entities;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.AutoMapper;
using Xunit;

namespace Tnf.Architecture.Mapper.Tests
{
    public class DtoToPocoProfileTests : TnfAppIntegratedTestBase<MapperTestModule>
    {
        [Fact]
        public void MapTo_CountryDto_To_CountryPoco()
        {
            var dto = new CountryDto()
            {
                Id = 1,
                Name = "Brasil"
            };

            var mappPoco = dto.MapTo<CountryPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(mappPoco.Id, mappPoco.Id);
            Assert.Equal(mappPoco.Name, mappPoco.Name);
        }

        [Fact]
        public void MapTo_ProfessionalDto_To_ProfessionalPoco()
        {
            var dto = new ProfessionalDto()
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

            var mappPoco = dto.MapTo<ProfessionalPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(dto.ProfessionalId, mappPoco.ProfessionalId);
            Assert.Equal(dto.Code, mappPoco.Code);
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
            var dto = new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Geral"
            };

            var mappPoco = dto.MapTo<SpecialtyPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(mappPoco.Id, mappPoco.Id);
            Assert.Equal(mappPoco.Description, mappPoco.Description);
        }

        [Fact]
        public void MapTo_PresidentDto_To_PresidentPoco()
        {
            var dto = new PresidentDto()
            {
                Id = "1234",
                Name = "George",
                Address = new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))
            };

            var mappPoco = dto.MapTo<PresidentPoco>();

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
