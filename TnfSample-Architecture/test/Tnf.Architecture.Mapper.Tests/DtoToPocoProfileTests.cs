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

            CountryPoco mappDto = dto.MapTo<CountryPoco>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, mappDto.Id);
            Assert.Equal(mappDto.Name, mappDto.Name);
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

            ProfessionalPoco mappDto = dto.MapTo<ProfessionalPoco>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, mappDto.Id);
            Assert.Equal(mappDto.Name, mappDto.Name);
            Assert.Equal(dto.Address.Complement, mappDto.AddressComplement);
            Assert.Equal(dto.Address.Number, mappDto.AddressNumber);
            Assert.Equal(dto.Address.Street, mappDto.Address);
            Assert.Equal(dto.Address.ZipCode.Number, mappDto.ZipCode);
            Assert.Equal(dto.Code, mappDto.Code);
            Assert.Equal(dto.Email, mappDto.Email);
            Assert.Equal(dto.Phone, mappDto.Phone);
            Assert.Equal(dto.ProfessionalId, mappDto.ProfessionalId);
        }

        [Fact]
        public void MapTo_ProfessionalCreateDto_To_ProfessionalPoco()
        {
            ProfessionalCreateDto dto = new ProfessionalCreateDto()
            {
                Address = new Address("Rua de teste", "123", "Compelemento", new ZipCode("12345678")),
                Email = "email@email.com",
                Name = "Professional teste",
                Phone = "988765541",
                ProfessionalId = 1,
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description = "Especialidade" }
                }
            };

            ProfessionalPoco mappDto = dto.MapTo<ProfessionalPoco>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, mappDto.Id);
            Assert.Equal(mappDto.Name, mappDto.Name);
            Assert.Equal(dto.Address.Complement, mappDto.AddressComplement);
            Assert.Equal(dto.Address.Number, mappDto.AddressNumber);
            Assert.Equal(dto.Address.Street, mappDto.Address);
            Assert.Equal(dto.Address.ZipCode.Number, mappDto.ZipCode);
            Assert.Equal(dto.Email, mappDto.Email);
            Assert.Equal(dto.Phone, mappDto.Phone);
            Assert.Equal(dto.ProfessionalId, mappDto.ProfessionalId);
        }

        [Fact]
        public void MapTo_SpecialtyDto_To_SpecialtyPoco()
        {
            SpecialtyDto dto = new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Geral"
            };

            SpecialtyPoco mappDto = dto.MapTo<SpecialtyPoco>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, mappDto.Id);
            Assert.Equal(mappDto.Description, mappDto.Description);
        }

        [Fact]
        public void MapTo_PresidentDto_To_PresidentPoco()
        {
            PresidentDto dto = new PresidentDto()
            {
                Id = "1234",
                Name = "George",
                ZipCode = new ZipCode("12345678")
            };

            PresidentPoco mappDto = dto.MapTo<PresidentPoco>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, mappDto.Id);
            Assert.Equal(mappDto.Name, mappDto.Name);
            Assert.Equal(mappDto.ZipCode, mappDto.ZipCode);
        }
    }
}
