using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Events.WhiteHouse;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
using Xunit;

namespace Tnf.Architecture.Domain.Tests.WhiteHouse
{
    public class WhiteHouseServiceTests : TnfAppIntegratedTestBase<DomainModuleTest>
    {
        private readonly IWhiteHouseService _whiteHouseService;

        public WhiteHouseServiceTests()
        {
            _whiteHouseService = Resolve<IWhiteHouseService>();
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _whiteHouseService.ShouldNotBeNull();
        }

        [Fact]
        public async Task WhiteHouse_Service_Return_All_Values()
        {
            // Arrange
            var requestDto = new GetAllPresidentsDto()
            {
                PageSize = 2
            };

            // Act
            var allPresidents = await _whiteHouseService.GetAllPresidents(requestDto);

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(allPresidents.Items.Count == 2);
        }

        [Fact]
        public async Task WhiteHouse_Service_Return_PresidentById()
        {
            // Act
            var response = await _whiteHouseService.GetPresidentById(new RequestDto<string>("1"));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(response.Id == "1");
            Assert.True(response.Name == "George Washington");
        }

        [Fact]
        public async Task WhiteHouse_Service_Not_Return_Non_Existing_President()
        {
            // Act
            await _whiteHouseService.GetPresidentById(new RequestDto<string>("99"));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task WhiteHouse_Service_Delete_President()
        {
            // Act
            await _whiteHouseService.DeletePresidentAsync("1");

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public async Task WhiteHouse_Service_Delete_Not_Accept_Non_Existing_President()
        {
            // Act
            await _whiteHouseService.DeletePresidentAsync("99");

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task WhiteHouse_Service_Insert_Valid_Presidents()
        {
            //Arrange
            LocalEventBus.Register<PresidentCreatedEvent>(
                eventData =>
                {
                    var president = eventData.President;
                    Assert.True(president.Id == "1" || president.Id == "2");
                });

            // Act
            await _whiteHouseService.InsertPresidentAsync(new PresidentDto("1", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))));

            // Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public async Task WhiteHouse_Service_Insert_Not_Accept_Invalid_Presidents()
        {
            // Act
            await _whiteHouseService.InsertPresidentAsync(new PresidentDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressNumberMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task WhiteHouse_Service_Update_Valid_President()
        {
            // Act
            var responseBase = await _whiteHouseService.UpdatePresidentAsync(new PresidentDto("1", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.True(responseBase.Name == "George Washington");
        }

        [Fact]
        public async Task WhiteHouse_Service_Update_Not_Accept_Invalid_President()
        {
            // Act
            await _whiteHouseService.UpdatePresidentAsync(new PresidentDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentAddressNumberMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task WhiteHouse_Service_Update_Not_Accept_Non_Existing_President()
        {
            // Act
            await _whiteHouseService.UpdatePresidentAsync(new PresidentDto("99", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }
    }
}
