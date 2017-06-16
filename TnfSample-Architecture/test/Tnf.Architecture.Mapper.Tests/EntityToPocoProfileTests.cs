using Tnf.Architecture.EntityFrameworkCore.Entities;
using Xunit;
using Tnf.AutoMapper;
using System;
using Tnf.Architecture.Data.Entities;
using Tnf.Architecture.Dto.Helpers;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.App.TestBase;

namespace Tnf.Architecture.Mapper.Tests
{
    public class EntityToPocoProfileTests : TnfAppIntegratedTestBase<MapperTestModule>
    {
        [Fact]
        public void MapTo_Professional_To_ProfessionalPoco()
        {
            var builder = new ProfessionalBuilder(LocalNotification)
                                .WithAddress(new Address("Rua de teste", "123", "Complement", new ZipCode("12345678")))
                                .WithProfessionalId(1)
                                .WithCode(Guid.NewGuid())
                                .WithEmail("email@email.com")
                                .WithName("Professional");

            Professional entity = builder.Build();

            ProfessionalPoco mappPoco = entity.MapTo<ProfessionalPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(entity.ProfessionalId, mappPoco.ProfessionalId);
            Assert.Equal(entity.Code, mappPoco.Code);
            Assert.Equal(entity.Name, mappPoco.Name);
            Assert.Equal(entity.Email, mappPoco.Email);
            Assert.Equal(entity.Phone, mappPoco.Phone);
            Assert.NotNull(mappPoco.Address);
            Assert.Equal(entity.Address.Complement, mappPoco.AddressComplement);
            Assert.Equal(entity.Address.Number, mappPoco.AddressNumber);
            Assert.Equal(entity.Address.Street, TextHelper.ToTitleCase(mappPoco.Address));
            Assert.Equal(entity.Address.ZipCode.Number, mappPoco.ZipCode);
        }

        [Fact]
        public void MapTo_SpecialtyPoco_To_SpecialtyDto()
        {
            var builder = new SpecialtyBuilder(LocalNotification)
                                .WithId(1)
                                .WithDescription("Cirurgia Geral");

            Specialty entity = builder.Build();

            SpecialtyPoco mappPoco = entity.MapTo<SpecialtyPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(mappPoco.Id, entity.Id);
            Assert.Equal(mappPoco.Description, entity.Description);
        }

        [Fact]
        public void MapTo_PresidentPoco_To_PresidentDto()
        {
            var builder = new PresidentBuilder(LocalNotification)
                                .WithId("1234")
                                .WithName("George")
                                .WithAddress(new Address("Rua de Teste", "123", "APT 12", new ZipCode("12345678")));

            President entity = builder.Build();

            PresidentPoco mappPoco = entity.MapTo<PresidentPoco>();

            Assert.NotNull(mappPoco);
            Assert.Equal(entity.Id, mappPoco.Id);
            Assert.Equal(entity.Name, mappPoco.Name);
            Assert.NotNull(mappPoco.Address);
            Assert.Equal(entity.Address.Complement, mappPoco.Address.Complement);
            Assert.Equal(entity.Address.Number, mappPoco.Address.Number);
            Assert.Equal(entity.Address.Street, mappPoco.Address.Street);
            Assert.Equal(entity.Address.ZipCode.Number, mappPoco.Address.ZipCode.Number);
        }
    }
}
