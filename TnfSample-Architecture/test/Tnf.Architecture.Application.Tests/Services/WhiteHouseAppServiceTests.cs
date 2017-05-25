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
            Assert.True(response.Success);
            Assert.IsType(typeof(SuccessResponseListDto<PresidentDto>), response);
            var successResponse = response as SuccessResponseListDto<PresidentDto>;
            successResponse.Items.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Get_All_Presidents_With_Error()
        {
            //Act
            var response = await _whiteHouseAppService.GetAllPresidents(new GetAllPresidentsDto());

            //Assert
            Assert.False(response.Success);
            Assert.IsType(typeof(ErrorResponseDto), response);
            var errorResponse = response as ErrorResponseDto;
            errorResponse.Message.ShouldBe("Invalid parameter");
            errorResponse.DetailedMessage.ShouldBe("Invalid parameter: PageSize");
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

            response = await _whiteHouseAppService.UpdatePresidentAsync(president.Id, new PresidentDto(
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
            //Act
            var response = await _whiteHouseAppService.UpdatePresidentAsync("99", new PresidentDto(
                "99",
                "New President",
                new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))));

            // Assert
            Assert.False(response.Success);
            Assert.IsType(typeof(ErrorResponseDto), response);
            var errorResponse = response as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task Should_Get_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.GetPresidentById(new RequestDto<string>("1"));

            // Assert
            Assert.True(response.Success);
            Assert.IsType(typeof(PresidentDto), response);
            var successResponse = response as PresidentDto;
            successResponse.Id.ShouldBe("1");
            successResponse.Name.ShouldBe("New President");
        }

        [Fact]
        public async Task Should_Get_President_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.GetPresidentById(new RequestDto<string>("2"));

            // Assert
            Assert.False(response.Success);
            Assert.IsType(typeof(ErrorResponseDto), response);
            var errorResponse = response as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task Should_Delete_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.DeletePresidentAsync("1");

            // Assert
            Assert.True(response.Success);
            Assert.IsType(typeof(SuccessResponseDto), response);
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
