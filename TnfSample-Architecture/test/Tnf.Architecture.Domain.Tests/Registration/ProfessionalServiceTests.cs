using Shouldly;
using System;
using System.Linq;
using Tnf.App.Dto.Request;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.Registration
{
    public class ProfessionalServiceTests : TnfAppIntegratedTestBase<DomainModuleTest>
    {
        private readonly IProfessionalService _profissionalService;

        public ProfessionalServiceTests()
        {
            _profissionalService = Resolve<IProfessionalService>();
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _profissionalService.ShouldNotBeNull();
        }

        [Fact]
        public void Professional_Service_Return_All_Values()
        {
            // Arrange
            var requestDto = new GetAllProfessionalsDto()
            {
                PageSize = 2
            };

            // Act
            var allProfessionals = _profissionalService.GetAllProfessionals(requestDto);

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(allProfessionals.Items.Count == 1);
        }

        [Fact]
        public void Professional_Service_Return_Professional()
        {
            // Act
            var response = _profissionalService.GetProfessional(new RequestDto<ProfessionalKeysDto>(new ProfessionalKeysDto(1, Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"))));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(response.ProfessionalId == 1);
            Assert.True(response.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"));
            Assert.True(response.Name == "João da Silva");
        }

        [Fact]
        public void Professional_Service_Not_Return_Non_Existing_Professional()
        {
            // Act
            _profissionalService.GetProfessional(new RequestDto<ProfessionalKeysDto>(new ProfessionalKeysDto(99, Guid.NewGuid())));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            Assert.True(LocalNotification.GetAll().Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public void Professional_Service_Delete_Professional()
        {
            // Act
            _profissionalService.DeleteProfessional(new ProfessionalKeysDto(1, Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")));

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Professional_Service_Delete_Not_Accept_Non_Existing_Professional()
        {
            // Act
            _profissionalService.DeleteProfessional(new ProfessionalKeysDto(99, Guid.NewGuid()));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public void Professional_Service_Insert_Valid_Professional()
        {
            // Act
            var responseBase = _profissionalService.CreateProfessional(new ProfessionalDto()
            {
                Address = new Address("Rua do comercio 2", "1233", "APT 1234", new ZipCode("22888888")),
                Email = "email2@email2.com",
                Name = "João da Silva",
                Phone = "15398264438"
            });

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(responseBase.Name == "João da Silva");
        }

        [Fact]
        public void Professional_Service_Insert_Not_Accept_Invalid_Professional()
        {
            // Act
            _profissionalService.CreateProfessional(new ProfessionalDto());

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
        public void Professional_Service_Update_Valid_Professional()
        {
            // Act
            var responseBase = _profissionalService.UpdateProfessional(new ProfessionalDto()
            {
                ProfessionalId = 1,
                Name = "João da Silva",
                Phone = "99997654",
                Email = "email@email.com",
                Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                Address = new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321"))
            });

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(responseBase.Name == "João da Silva");
        }

        [Fact]
        public void Professional_Service_Update_Not_Accept_Invalid_Professional()
        {
            // Act
            _profissionalService.UpdateProfessional(new ProfessionalDto());

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
        public void Professional_Service_Update_Not_Accept_Non_Existing_Professional()
        {
            // Act
            _profissionalService.UpdateProfessional(new ProfessionalDto()
            {
                ProfessionalId = 99,
                Name = "João da Silva",
                Phone = "99997654",
                Email = "email@email.com",
                Code = Guid.NewGuid(),
                Address = new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321"))
            });

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

    }
}
