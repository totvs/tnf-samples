using System.Threading.Tasks;
using Tnf.Architecture.Web.Tests.App.Controllers;
using Xunit;
using Shouldly;
using Tnf.Web.Models;
using Tnf.Architecture.Dto;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Tnf.Architecture.Web.Controllers;

namespace Tnf.Architecture.Web.Tests.Tests
{
    public class WhiteHouseControllerTests : AppTestBase
    {
        [Fact]
        public void Should_Resolve_Controller()
        {
            ServiceProvider.GetService<WhiteHouseController>().ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAll_Presidents_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<PagingDtoResponse<PresidentDto>>>(
                               "/api/white-house?offset=0&pageSize=10",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Data.Count, 6);
            response.Result.Notifications.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetAll_Presidents_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                "/api/white-house?offset=0&pageSize=0",
                HttpStatusCode.BadRequest
                );

            response.Success.ShouldBeTrue();
            response.Result.ShouldBe($"Invalid parameter: pageSize");
        }

        [Fact]
        public async Task Get_President_Return_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<PresidentDto>>(
                               "/api/white-house/1",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.NotNull(response.Result.Id == "1");
            Assert.NotNull(response.Result.Name == "George Washington");
            Assert.NotNull(response.Result.ZipCode.Number == "12345678");
        }

        [Fact]
        public async Task Get_President_Not_Exist_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                               "/api/white-house/0",
                               HttpStatusCode.BadRequest
                           );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.Equal(response.Result, "President not found");
        }
    }
}
