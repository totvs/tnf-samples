using System;
using System.Collections.Generic;
using Tnf.App.AutoMapper;
using Tnf.App.TestBase;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.WhiteHouse;
using Xunit;

namespace Tnf.Architecture.Mapper.Tests.Profiles
{
    public class EntityToDtoProfileTests : TnfAppIntegratedTestBase<MapperTestModule>
    {
        [Fact]
        public void MapTo_Professional_To_ProfessionalDto()
        {
            var entity = new Professional
            {
                Address = new Address("Rua de teste", "123", "Complement", new ZipCode("12345678")),
                ProfessionalId = 1,
                Code = Guid.NewGuid(),
                Email = "email@email.com",
                Name = "Professional",
                Specialties = new List<Specialty>
                {
                    new Specialty { Id = 1, Description = "Especialidade" }
                }
            };

            var mappDto = entity.MapTo<ProfessionalDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(entity.ProfessionalId, mappDto.ProfessionalId);
            Assert.Equal(entity.Code, mappDto.Code);
            Assert.Equal(entity.Name, mappDto.Name);
            Assert.Equal(entity.Email, mappDto.Email);
            Assert.Equal(entity.Phone, mappDto.Phone);
            Assert.NotNull(mappDto.Address);
            Assert.Equal(entity.Address.Complement, mappDto.Address.Complement);
            Assert.Equal(entity.Address.Number, mappDto.Address.Number);
            Assert.Equal(entity.Address.Street, entity.Address.Street);
            Assert.Equal(entity.Address.ZipCode.Number, mappDto.Address.ZipCode.Number);
            Assert.NotNull(mappDto.Specialties[0]);
            Assert.Equal(entity.Specialties[0].Id, mappDto.Specialties[0].Id);
            Assert.Equal(entity.Specialties[0].Description, mappDto.Specialties[0].Description);
        }

        [Fact]
        public void MapTo_Specialty_To_SpecialtyDto()
        {
            var entity = new Specialty
            {
                Id = 1,
                Description = "Cirurgia Geral"
            };

            var mappDto = entity.MapTo<SpecialtyDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, entity.Id);
            Assert.Equal(mappDto.Description, entity.Description);
        }

        [Fact]
        public void MapTo_President_To_PresidentDto()
        {
            var entity = new President
            {
                Id = "1234",
                Name = "George",
                Address = new Address("Rua de Teste", "123", "APT 12", new ZipCode("12345678"))
            };

            var mappDto = entity.MapTo<PresidentDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(entity.Id, mappDto.Id);
            Assert.Equal(entity.Name, mappDto.Name);
            Assert.NotNull(mappDto.Address);
            Assert.Equal(entity.Address.Complement, mappDto.Address.Complement);
            Assert.Equal(entity.Address.Number, mappDto.Address.Number);
            Assert.Equal(entity.Address.Street, mappDto.Address.Street);
            Assert.Equal(entity.Address.ZipCode.Number, mappDto.Address.ZipCode.Number);
        }

        [Fact]
        public void MapTo_Person_To_PersonDto()
        {
            var entity = new Person
            {
                Id = 1,
                Name = "John Doe"
            };

            var mappDto = entity.MapTo<PersonDto>();

            Assert.NotNull(mappDto);
            Assert.Equal(mappDto.Id, entity.Id);
            Assert.Equal(mappDto.Name, entity.Name);
        }
    }
}
