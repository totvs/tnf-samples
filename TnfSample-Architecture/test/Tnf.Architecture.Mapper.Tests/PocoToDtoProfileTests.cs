using Tnf.Architecture.Dto;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.TestBase;
using Xunit;
using Tnf.AutoMapper;
using System;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Data.Entities;
using Tnf.Architecture.Dto.Helpers;
using Tnf.Architecture.Dto.ValueObjects;
using System.Collections.Generic;

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
                ProfessionalId = 1,
                ProfessionalSpecialties = new List<ProfessionalSpecialtiesPoco>()
                {
                    new ProfessionalSpecialtiesPoco()
                    {
                        Specialty = new SpecialtyPoco()
                        {
                            Id = 1,
                            Description = "Especialidade teste"
                        },
                        SpecialtyId = 1
                    }
                }
            };

            ProfessionalDto mappDto = poco.MapTo<ProfessionalDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(poco.ProfessionalId, mappDto.ProfessionalId);
            Assert.Equal(poco.Code, mappDto.Code);
            Assert.Equal(poco.Name, mappDto.Name);
            Assert.Equal(poco.Email, mappDto.Email);
            Assert.Equal(poco.Phone, mappDto.Phone);
            Assert.NotNull(mappDto.Address);
            Assert.Equal(poco.AddressComplement, mappDto.Address.Complement);
            Assert.Equal(poco.AddressNumber, mappDto.Address.Number);
            Assert.Equal(TextHelper.ToTitleCase(poco.Address), mappDto.Address.Street);
            Assert.Equal(poco.ZipCode, mappDto.Address.ZipCode.Number);
            Assert.NotNull(mappDto.Specialties);
            Assert.NotEmpty(mappDto.Specialties);
            Assert.Equal(poco.ProfessionalSpecialties.Count, mappDto.Specialties.Count);
            Assert.NotNull(mappDto.Specialties[0]);
            Assert.Equal(poco.ProfessionalSpecialties[0].Specialty.Id, mappDto.Specialties[0].Id);
            Assert.Equal(poco.ProfessionalSpecialties[0].Specialty.Description, mappDto.Specialties[0].Description);
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
            Assert.Equal(mappDto.Id, poco.Id);
            Assert.Equal(mappDto.Description, poco.Description);
        }

        [Fact]
        public void MapTo_PresidentPoco_To_PresidentDto()
        {
            PresidentPoco poco = new PresidentPoco()
            {
                Id = "1234",
                Name = "George",
                Address = new Address("Rua de Teste", "123", "APT 12", new ZipCode("12345678"))
            };

            PresidentDto mappDto = poco.MapTo<PresidentDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(poco.Id, mappDto.Id);
            Assert.Equal(poco.Name, mappDto.Name);
            Assert.NotNull(mappDto.Address);
            Assert.Equal(poco.Address.Complement, mappDto.Address.Complement);
            Assert.Equal(poco.Address.Number, mappDto.Address.Number);
            Assert.Equal(poco.Address.Street, mappDto.Address.Street);
            Assert.Equal(poco.Address.ZipCode.Number, mappDto.Address.ZipCode.Number);
        }
    }
}
