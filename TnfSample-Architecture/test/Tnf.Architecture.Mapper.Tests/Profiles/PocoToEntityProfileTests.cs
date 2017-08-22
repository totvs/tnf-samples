using System;
using System.Collections.Generic;
using Tnf.App.TestBase;
using Tnf.Architecture.Carol.Entities;
using Tnf.Architecture.Common.Helpers;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.AutoMapper;
using Xunit;

namespace Tnf.Architecture.Mapper.Tests.Profiles
{
    public class PocoToEntityProfileTests : TnfAppIntegratedTestBase<MapperTestModule>
    {
        [Fact]
        public void MapTo_ProfessionalPoco_To_Professional()
        {
            var poco = new ProfessionalPoco
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
                ProfessionalSpecialties = new List<ProfessionalSpecialtiesPoco>
                {
                    new ProfessionalSpecialtiesPoco
                    {
                        Specialty = new SpecialtyPoco
                        {
                            Id = 1,
                            Description = "Especialidade teste"
                        },
                        SpecialtyId = 1
                    }
                }
            };

            var mappEntity = poco.MapTo<Professional>();

            Assert.NotNull(mappEntity);
            Assert.Equal(poco.ProfessionalId, mappEntity.ProfessionalId);
            Assert.Equal(poco.Code, mappEntity.Code);
            Assert.Equal(poco.Name, mappEntity.Name);
            Assert.Equal(poco.Email, mappEntity.Email);
            Assert.Equal(poco.Phone, mappEntity.Phone);
            Assert.NotNull(mappEntity.Address);
            Assert.Equal(poco.AddressComplement, mappEntity.Address.Complement);
            Assert.Equal(poco.AddressNumber, mappEntity.Address.Number);
            Assert.Equal(TextHelper.ToTitleCase(poco.Address), mappEntity.Address.Street);
            Assert.Equal(poco.ZipCode, mappEntity.Address.ZipCode.Number);
            Assert.NotNull(mappEntity.Specialties);
            Assert.NotEmpty(mappEntity.Specialties);
            Assert.Equal(poco.ProfessionalSpecialties.Count, mappEntity.Specialties.Count);
            Assert.NotNull(mappEntity.Specialties[0]);
            Assert.Equal(poco.ProfessionalSpecialties[0].Specialty.Id, mappEntity.Specialties[0].Id);
            Assert.Equal(poco.ProfessionalSpecialties[0].Specialty.Description, mappEntity.Specialties[0].Description);
        }

        [Fact]
        public void MapTo_SpecialtyPoco_To_Specialty()
        {
            var poco = new SpecialtyPoco
            {
                Id = 1,
                Description = "Cirurgia Geral"
            };

            var mappEntity = poco.MapTo<Specialty>();

            Assert.NotNull(mappEntity);
            Assert.Equal(mappEntity.Id, poco.Id);
            Assert.Equal(mappEntity.Description, poco.Description);
        }

        [Fact]
        public void MapTo_PresidentPoco_To_PresidentDto()
        {
            var poco = new PresidentPoco
            {
                Id = "1234",
                Name = "George",
                Address = new Address("Rua de Teste", "123", "APT 12", new ZipCode("12345678"))
            };

            var mappEntity = poco.MapTo<President>();

            Assert.NotNull(mappEntity);
            Assert.Equal(poco.Id, mappEntity.Id);
            Assert.Equal(poco.Name, mappEntity.Name);
            Assert.NotNull(mappEntity.Address);
            Assert.Equal(poco.Address.Complement, mappEntity.Address.Complement);
            Assert.Equal(poco.Address.Number, mappEntity.Address.Number);
            Assert.Equal(poco.Address.Street, mappEntity.Address.Street);
            Assert.Equal(poco.Address.ZipCode.Number, mappEntity.Address.ZipCode.Number);
        }
    }
}
