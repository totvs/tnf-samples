using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.App.Dto.Request;
using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common.Enumerables;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Xunit;

namespace Tnf.Architecture.Application.Tests.Services
{
    public sealed class ProfessionalAppServiceTests : TnfEfCoreIntegratedTestBase<EfCoreAppTestModule>
    {
        private readonly IProfessionalAppService _professionalAppService;
        private readonly ProfessionalPoco _professionalPoco;

        public ProfessionalAppServiceTests()
        {
            _professionalAppService = Resolve<IProfessionalAppService>();

            _professionalPoco = new ProfessionalPoco()
            {
                ProfessionalId = 1,
                Code = Guid.NewGuid(),
                Address = "Rua do comercio",
                AddressNumber = "123",
                AddressComplement = "APT 123",
                Email = "email@email.com",
                Name = "João da Silva",
                Phone = "55998765432",
                ZipCode = "99888777"
            };

            // Setup
            UsingDbContext<LegacyDbContext>(context =>
            {
                context.Professionals.Add(_professionalPoco);
                context.Specialties.Add(new SpecialtyPoco() { Id = 1, Description = "Anestesiologia" });
            });
        }

        [Fact]
        public void Should_Get_All_Professionals_With_Success()
        {
            //Act
            var response = _professionalAppService.GetAllProfessionals(new GetAllProfessionalsDto() { PageSize = 10 });

            //Assert
            Assert.False(LocalNotification.HasNotification());
            response.Items.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Insert_Professional_With_Success()
        {
            //Arrange
            var professionalDto = new ProfessionalDto
            {
                ProfessionalId = 2,
                Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                Address = new Address("Rua teste", "98765", "APT 9876", new ZipCode("23156478")),
                Email = "email1234@email.com",
                Name = "Jose da Silva",
                Phone = "58962348",
                Specialties = new List<SpecialtyDto>
                {
                    new SpecialtyDto { Id = 1, Description = "Anestesiologia" }
                }
            };

            //Act
            var result = _professionalAppService.CreateProfessional(professionalDto);

            //Assert
            Assert.False(LocalNotification.HasNotification());
            result.ProfessionalId.ShouldBe(2);
        }

        [Fact]
        public void Should_Insert_Professional_With_Error()
        {
            // Act
            _professionalAppService.CreateProfessional(new ProfessionalDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public void Should_Insert_Null_Professional_With_Error()
        {
            // Act
            _professionalAppService.CreateProfessional(null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public void Should_Update_Professional_With_Success()
        {
            //Arrange
            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 2,
                Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                Address = new Address("Rua teste", "98765", "APT 9876", new ZipCode("23156478")),
                Email = "email1234@email.com",
                Name = "Jose da Silva",
                Phone = "58962348",
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description = "Anestesiologia" }
                }
            };

            //Act
            var result = _professionalAppService.CreateProfessional(professionalDto);

            //Assert
            Assert.False(LocalNotification.HasNotification());

            result.ProfessionalId.ShouldBe(2);

            result.Name = "Nome Alterado Teste";

            result.Specialties.Clear();
            result = _professionalAppService.UpdateProfessional(new ComposeKey<Guid, decimal>(result.Code, result.ProfessionalId), result);

            //Assert
            Assert.False(LocalNotification.HasNotification());
            result.Name.ShouldBe("Nome Alterado Teste");
        }

        [Fact]
        public void Should_Update_Professional_With_Error()
        {
            // Arrange
            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 99,
                Code = Guid.NewGuid(),
                Address = new Address("Rua teste", "98765", "APT 9876", new ZipCode("23156478")),
                Email = "email1234@email.com",
                Name = "Jose da Silva",
                Phone = "58962348",
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description = "Anestesiologia" }
                }
            };

            //Act
            _professionalAppService.UpdateProfessional(new ComposeKey<Guid, decimal>(professionalDto.Code, professionalDto.ProfessionalId), professionalDto);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public void Should_Update_Invalid_Id_With_Error()
        {
            // Act
            _professionalAppService.UpdateProfessional(new ComposeKey<Guid, decimal>(Guid.Empty, 0), new ProfessionalDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.Equal(notifications.Count, 2);
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public void Should_Update_Null_Professional_With_Error()
        {
            // Act
            _professionalAppService.UpdateProfessional(new ComposeKey<Guid, decimal>(Guid.NewGuid(), 1), null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.Equal(notifications.Count, 1);
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public void Should_Get_Professional_With_Success()
        {
            //Act
            var response = _professionalAppService.GetProfessional(new RequestDto<ComposeKey<Guid, decimal>>(new ComposeKey<Guid, decimal>(_professionalPoco.Code, 1)));

            //Assert
            Assert.False(LocalNotification.HasNotification());
            response.ProfessionalId.ShouldBe(1);
            response.Code.ShouldBe(_professionalPoco.Code);
        }

        [Fact]
        public void Should_Get_Professional_With_Error()
        {
            // Act
            var response = _professionalAppService.GetProfessional(new RequestDto<ComposeKey<Guid, decimal>>(new ComposeKey<Guid, decimal>(_professionalPoco.Code, 99)));

            // Assert
            Assert.Null(response);
            Assert.True(LocalNotification.HasNotification());
            Assert.True(LocalNotification.GetAll().Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public void Should_Delete_Professional_With_Success()
        {
            //Act
            _professionalAppService.DeleteProfessional(new ComposeKey<Guid, decimal>(_professionalPoco.Code, 1));

            var successResponse = _professionalAppService.GetAllProfessionals(new GetAllProfessionalsDto() { PageSize = 10 });

            //Assert
            Assert.False(LocalNotification.HasNotification());
            successResponse.Items.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Delete_Professional_With_Error()
        {
            // Act
            _professionalAppService.DeleteProfessional(new ComposeKey<Guid, decimal>(_professionalPoco.Code, 99));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }
    }
}
