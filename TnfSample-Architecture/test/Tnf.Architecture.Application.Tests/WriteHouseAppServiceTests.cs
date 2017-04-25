using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Application.Services;
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
        public async Task Should_Delete_President_With_Success()
        {
            // Act
            var response = await _whiteHouseAppService.DeletePresidentAsync("6");

            // Assert
            Assert.True(response.Success);
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
    }
}
