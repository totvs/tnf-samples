using Shouldly;
using System.Threading.Tasks;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
using Xunit;
using System.Linq;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Application.Tests.Services
{
    public class WriteHouseAppServiceTests : NSubstituteAppTestBase
    {
        private readonly IWhiteHouseAppService _whiteHouseAppService;

        public WriteHouseAppServiceTests()
        {
            _whiteHouseAppService = LocalIocManager.Resolve<IWhiteHouseAppService>();
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _whiteHouseAppService.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Get_President_With_Success()
        {
            // Act
            var result = await _whiteHouseAppService.GetPresidentById(new RequestDto<string>("1"));

            // Assert
            result.Id.ShouldBe("1");
            result.Name.ShouldBe("New President");
        }

        [Fact]
        public async Task Should_Get_President_With_Error()
        {
            // Act
            var result = await _whiteHouseAppService.GetPresidentById(new RequestDto<string>("2"));

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task Should_Get_All_Presidents_With_Success()
        {
            // Arrange
            var requestDto = new GetAllPresidentsDto();

            // Act
            var result = await _whiteHouseAppService.GetAllPresidents(requestDto);

            // Assert
            result.Items.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Insert_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(new PresidentDto("1", "New President", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))), true);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task Should_Insert_President_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(new PresidentDto());

            // Assert
            Assert.False(response.Success);
            Assert.IsType(typeof(ErrorResponseDto), response);
            var errorResponse = response as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.PresidentAddressNumberMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Should_Update_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(new PresidentDto("1", "New President", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))), true);

            Assert.True(response.Success);
            Assert.IsType(typeof(PresidentDto), response);
            var president = response as PresidentDto;

            president.Name.ShouldBe("New President");

            response = await _whiteHouseAppService.UpdatePresidentAsync(new PresidentDto(
                president.Id,
                "Alter President",
                president.Address));

            // Assert
            Assert.True(response.Success);
            Assert.IsType(typeof(PresidentDto), response);
            president = response as PresidentDto;

            president.Name.ShouldBe("Alter President");
        }

        [Fact]
        public async Task Should_Update_President_With_Error()
        {
            // Arrange
            var presidentDto = new PresidentDto("1", "New President", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678")));

            //Act
            var response = await _whiteHouseAppService.UpdatePresidentAsync(new PresidentDto(
                "99",
                presidentDto.Name,
                presidentDto.Address));

            // Assert
            Assert.False(response.Success);
            Assert.IsType(typeof(ErrorResponseDto), response);
            var errorResponse = response as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task Should_Delete_President_With_Success()
        {
            // Arrange
            var response = await _whiteHouseAppService.InsertPresidentAsync(new PresidentDto("1", "New President", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))), true);

            // Act
            Assert.IsType(typeof(PresidentDto), response);
            var president = response as PresidentDto;

            response = await _whiteHouseAppService.DeletePresidentAsync(president.Id);

            var pagedResult = await _whiteHouseAppService.GetAllPresidents(new GetAllPresidentsDto());

            // Assert
            Assert.True(response.Success);
            pagedResult.Total.ShouldBe(0);
        }

        [Fact]
        public async Task Should_Delete_President_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.DeletePresidentAsync("99");

            // Assert
            Assert.False(response.Success);
            Assert.IsType(typeof(ErrorResponseDto), response);
            var errorResponse = response as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }
    }
}
