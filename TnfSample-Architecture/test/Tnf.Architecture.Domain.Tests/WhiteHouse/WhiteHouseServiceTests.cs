using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto;
using Xunit;
using System.Linq;
using Shouldly;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Events.WhiteHouse;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Domain.Tests.WhiteHouse
{
    public class WhiteHouseServiceTests : TestBaseWithLocalIocManager
    {
        IWhiteHouseService _whiteHouseService;
        readonly IWhiteHouseRepository _whiteHouseRepository;

        public WhiteHouseServiceTests()
        {
            _whiteHouseRepository = Substitute.For<IWhiteHouseRepository>();

            var presidentDto = new PresidentDto("1", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678")));

            var presidentList = new List<PresidentDto>()
            {
                presidentDto,
                new PresidentDto("2", "Bill Clinton", new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321")))
            };

            var presidentPaging = new PagingResponseDto<PresidentDto>(presidentList);

            _whiteHouseRepository.GetAllPresidents(Arg.Any<GetAllPresidentsDto>())
                .Returns(Task.FromResult(presidentPaging));

            _whiteHouseRepository.GetPresidentById(Arg.Is("1"))
                .Returns(Task.FromResult(presidentDto));

            _whiteHouseRepository.InsertPresidentsAsync(Arg.Any<List<PresidentDto>>())
                .Returns(Task.FromResult(presidentList));

            _whiteHouseRepository.UpdatePresidentsAsync(Arg.Is<PresidentDto>(p => p.Id == "1"))
                .Returns(Task.FromResult(presidentDto));

            _whiteHouseRepository.DeletePresidentsAsync(Arg.Is("1"))
                .Returns(Task.FromResult(true));

            _whiteHouseService = new WhiteHouseService(_whiteHouseRepository, EventBus);
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
            var requestDto = new GetAllPresidentsDto(0, 2);

            // Act
            var allPresidents = await _whiteHouseService.GetAllPresidents(requestDto);

            // Assert
            Assert.True(allPresidents.Success);
            Assert.Empty(allPresidents.Notifications);
            Assert.True(allPresidents.Data.Count == 2);
        }

        [Fact]
        public async Task WhiteHouse_Service_Return_PresidentById()
        {
            // Act
            var president = await _whiteHouseService.GetPresidentById("1");

            // Assert
            Assert.True(president.Id == "1");
            Assert.True(president.Name == "George Washington");
        }

        [Fact]
        public async Task WhiteHouse_Service_Not_Return_Non_Existing_President()
        {
            // Act
            var president = await _whiteHouseService.GetPresidentById("99");

            // Assert
            Assert.Null(president);
        }

        [Fact]
        public async Task WhiteHouse_Service_Delete_President()
        {
            // Act
            var response = await _whiteHouseService.DeletePresidentAsync("1");

            // Assert
            Assert.True(response.Success);
            Assert.Empty(response.Notifications);
        }

        [Fact]
        public async Task WhiteHouse_Service_Delete_Not_Accept_Non_Existing_President()
        {
            // Act
            var response = await _whiteHouseService.DeletePresidentAsync("99");

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task WhiteHouse_Service_Insert_Valid_Presidents()
        {
            //Arrange
            EventBus.Register<PresidentCreatedEvent>(
                eventData =>
                {
                    var president = eventData.President;
                    Assert.True(president.Id == "1" || president.Id == "2");
                });

            _whiteHouseService = new WhiteHouseService(_whiteHouseRepository, EventBus);

            // Act
            var responseBase = await _whiteHouseService.InsertPresidentAsync(new List<PresidentDto>()
            {
                new PresidentDto("1", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678")))
            });

            // Assert
            Assert.True(responseBase.Success);
            Assert.Empty(responseBase.Notifications);
            Assert.True(responseBase.Data.Count == 2);
        }

        [Fact]
        public async Task WhiteHouse_Service_Insert_Not_Accept_Invalid_Presidents()
        {
            // Act
            var responseBase = await _whiteHouseService.InsertPresidentAsync(new List<PresidentDto>()
            {
                    new PresidentDto()
            });

            // Assert
            Assert.False(responseBase.Success);
            Assert.Null(responseBase.Data);
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentAddressNumberMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task WhiteHouse_Service_Update_Valid_President()
        {
            // Act
            var responseBase = await _whiteHouseService.UpdatePresidentAsync(new PresidentDto("1", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))));

            // Assert
            Assert.True(responseBase.Success);
            Assert.Empty(responseBase.Notifications);
            Assert.True(responseBase.Data.Name == "George Washington");
        }

        [Fact]
        public async Task WhiteHouse_Service_Update_Not_Accept_Invalid_President()
        {
            // Act
            var responseBase = await _whiteHouseService.UpdatePresidentAsync(new PresidentDto());

            // Assert
            Assert.False(responseBase.Success);
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentAddressNumberMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task WhiteHouse_Service_Update_Not_Accept_Non_Existing_President()
        {
            // Act
            var responseBase = await _whiteHouseService.UpdatePresidentAsync(new PresidentDto("99", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))));

            // Assert
            Assert.False(responseBase.Success);
            Assert.Null(responseBase.Data);
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }
    }
}
