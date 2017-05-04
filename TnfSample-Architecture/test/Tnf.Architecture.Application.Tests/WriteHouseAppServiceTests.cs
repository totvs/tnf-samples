using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Xunit;

namespace Tnf.Architecture.Application.Tests
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
        public async Task Should_Delete_President_With_Success()
        {
            // Arrange
            var listResponse = await _whiteHouseAppService.InsertPresidentAsync(new List<PresidentDto>()
            {
                new PresidentDto("1", "New President", "12345678")
            }, true);

            // Act
            var response = await _whiteHouseAppService.DeletePresidentAsync(listResponse.Data[0].Id);

            var pagedResult = await _whiteHouseAppService.GetAllPresidents(new GellAllPresidentsRequestDto());

            // Assert
            Assert.True(response.Success);
            pagedResult.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Should_Insert_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(new List<PresidentDto>()
            {
                new PresidentDto("1", "New President", "12345678")
            }, true);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task Should_Insert_President_With_Error()
        {
            // Act
            var response = await _whiteHouseAppService.InsertPresidentAsync(new List<PresidentDto>()
            {
                new PresidentDto()
            });

            // Assert
            Assert.False(response.Success);
            Assert.Equal(response.Notifications.Count, 2);
        }

        [Fact]
        public async Task Should_Get_All_Presidents_With_Success()
        {
            var requestDto = new GellAllPresidentsRequestDto();

            var result = await _whiteHouseAppService.GetAllPresidents(requestDto);

            result.Data.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Get_President_With_Success()
        {
            var result = await _whiteHouseAppService.GetPresidentById("1");

            result.Id.ShouldBe("1");
            result.Name.ShouldBe("New President");
        }

        [Fact]
        public async Task Should_Get_President_With_Error()
        {
            var result = await _whiteHouseAppService.GetPresidentById("2");

            result.ShouldBeNull();
        }

        [Fact]
        public async Task Should_Insert_And_Update_President_With_Success()
        {
            // Act
            var listResponse = await _whiteHouseAppService.InsertPresidentAsync(new List<PresidentDto>()
            {
                new PresidentDto("1", "New President", "12345678")
            }, true);

            Assert.True(listResponse.Success);
            listResponse.Data[0].Name.ShouldBe("New President");

            var response = await _whiteHouseAppService.UpdatePresidentAsync(new PresidentDto(
                listResponse.Data[0].Id,
                listResponse.Data[0].Name,
                listResponse.Data[0].ZipCode.Number));

            Assert.True(response.Success);
        }
    }
}
