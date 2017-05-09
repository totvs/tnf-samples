﻿using Tnf.Architecture.Dto;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.TestBase;
using Xunit;
using Tnf.AutoMapper;
using System;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Data.Entities;
using Tnf.Architecture.Dto.Helpers;

namespace Tnf.Architecture.Mapper.Tests
{
    public class PocoToDtoProfileTests : TnfIntegratedTestBase<MapperTestModule>
    {
        [Fact]
        public void MapTo_CountryPoco_To_CountryDto()
        {
            CountryPoco poco = new CountryPoco()
            {
                Id = 1,
                Name = "Brasil"
            };

            CountryDto mappDto = poco.MapTo<CountryDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, poco.Id);
            Assert.Equal(mappDto.Name, poco.Name);
        }

        [Fact]
        public void MapTo_ProfessionalPoco_To_ProfessionalDto()
        {
            ProfessionalPoco poco = new ProfessionalPoco()
            {
                Address = "Rua de teste",
                AddressComplement = "Complement",
                AddressNumber = "123",
                ZipCode = "12345678",
                Code = Guid.NewGuid(),
                Email = "email@email.com",
                Name = "Professional",
                Phone = "987456321",
                ProfessionalId = 1
            };

            ProfessionalDto mappDto = poco.MapTo<ProfessionalDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(poco.ProfessionalId, mappDto.ProfessionalId);
            Assert.Equal(poco.Name, mappDto.Name);
            Assert.Equal(poco.AddressComplement, mappDto.Address.Complement);
            Assert.Equal(poco.AddressNumber, mappDto.Address.Number);
            Assert.Equal(TextHelper.ToTitleCase(poco.Address), mappDto.Address.Street);
            Assert.Equal(poco.ZipCode, mappDto.Address.ZipCode.Number);
            Assert.Equal(poco.Code, mappDto.Code);
            Assert.Equal(poco.Email, mappDto.Email);
            Assert.Equal(poco.Phone, mappDto.Phone);
            Assert.Equal(poco.ProfessionalId, mappDto.ProfessionalId);
        }

        [Fact]
        public void MapTo_ProfessionalPoco_To_ProfessionalCreateDto()
        {
            ProfessionalPoco poco = new ProfessionalPoco()
            {
                Address = "Rua de teste",
                AddressComplement = "Complement",
                AddressNumber = "123",
                ZipCode = "12345678",
                Code = Guid.NewGuid(),
                Email = "email@email.com",
                Name = "Professional",
                Phone = "987456321",
                ProfessionalId = 1
            };

            ProfessionalCreateDto mappDto = poco.MapTo<ProfessionalCreateDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(poco.ProfessionalId, mappDto.ProfessionalId);
            Assert.Equal(poco.Name, mappDto.Name);
            Assert.Equal(poco.AddressComplement, mappDto.Address.Complement);
            Assert.Equal(poco.AddressNumber, mappDto.Address.Number);
            Assert.Equal(TextHelper.ToTitleCase(poco.Address), mappDto.Address.Street);
            Assert.Equal(poco.ZipCode, mappDto.Address.ZipCode.Number);
            Assert.Equal(poco.Email, mappDto.Email);
            Assert.Equal(poco.Phone, mappDto.Phone);
            Assert.Equal(poco.ProfessionalId, mappDto.ProfessionalId);
        }

        [Fact]
        public void MapTo_SpecialtyPoco_To_SpecialtyDto()
        {
            SpecialtyPoco poco = new SpecialtyPoco()
            {
                Id = 1,
                Description = "Cirurgia Geral"
            };

            SpecialtyDto mappDto = poco.MapTo<SpecialtyDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, mappDto.Id);
            Assert.Equal(mappDto.Description, mappDto.Description);
        }

        [Fact]
        public void MapTo_PresidentPoco_To_PresidentDto()
        {
            PresidentPoco poco = new PresidentPoco()
            {
                Id = "1234",
                Name = "George",
                ZipCode = "12345678"
            };

            PresidentDto mappDto = poco.MapTo<PresidentDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, mappDto.Id);
            Assert.Equal(mappDto.Name, mappDto.Name);
            Assert.Equal(mappDto.ZipCode, mappDto.ZipCode);
        }
    }
}
