using Shouldly;
using System.Threading.Tasks;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Dto.Enumerables;
using Xunit;
using System.Linq;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.App.Bus.Notifications;

namespace Tnf.Architecture.Application.Tests.Services
{
    public class WhiteHouseAppServiceTests : NSubstituteAppTestBase
    {
        private readonly IWhiteHouseAppService _whiteHouseAppService;

        public WhiteHouseAppServiceTests()
        {
            _whiteHouseAppService = LocalIocManager.Resolve<IWhiteHouseAppService>();
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _whiteHouseAppService.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Get_All_Presidents_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.GetAllPresidents(new GetAllPresidentsDto() { PageSize = 10 });

            // Assert
            Assert.False(Notification.HasNotification());
            response.Items.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Get_All_Presidents_With_Error()
        {
            //Act
            var response = await _whiteHouseAppService.GetAllPresidents(new GetAllPresidentsDto());

            //Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameterDynamic.ToString()));
        }

        [Fact]
        public async Task Should_Insert_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(new PresidentDto("1", "New President", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))), true);

            // Assert
            Assert.False(Notification.HasNotification());
        }

        [Fact]
        public async Task Should_Insert_President_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(new PresidentDto());

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressNumberMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Should_Insert_Null_President_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(null);

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameterDynamic.ToString()));
        }

        [Fact]
        public async Task Should_Update_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(new PresidentDto("1", "New President", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))), true);

            Assert.False(Notification.HasNotification());

            response.Name.ShouldBe("New President");

            response = await _whiteHouseAppService.UpdatePresidentAsync(response.Id, new PresidentDto(
                response.Id,
                "Alter President",
                response.Address));

            // Assert
            Assert.False(Notification.HasNotification());

            response.Name.ShouldBe("Alter President");
        }

        [Fact]
        public async Task Should_Update_President_With_Error()
        {
            //Act
            var response = await _whiteHouseAppService.UpdatePresidentAsync("99", new PresidentDto(
                "99",
                "New President",
                new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))));

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task Should_Update_Invalid_Id_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.UpdatePresidentAsync("", new PresidentDto());

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameterDynamic.ToString()));
        }

        [Fact]
        public async Task Should_Update_Null_President_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.UpdatePresidentAsync("1", null);

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(n => n.Message == Error.InvalidParameterDynamic.ToString()));
        }

        [Fact]
        public async Task Should_Get_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.GetPresidentById(new RequestDto<string>("1"));

            // Assert
            Assert.False(Notification.HasNotification());
            response.Id.ShouldBe("1");
            response.Name.ShouldBe("New President");
        }

        [Fact]
        public async Task Should_Get_President_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.GetPresidentById(new RequestDto<string>("2"));

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task Should_Delete_President_With_Success()
        {
            // Act
            await _whiteHouseAppService.DeletePresidentAsync("1");

            // Assert
            Assert.False(Notification.HasNotification());
        }

        [Fact]
        public async Task Should_Delete_President_With_Error()
        {
            // Act
            await _whiteHouseAppService.DeletePresidentAsync("99");

            // Assert
            Assert.True(Notification.HasNotification());
            var notifications = Notification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }
    }
}
