using Xunit;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Tnf.Web.Models;
using Tnf.Application.Services.Dto;
using Tnf.Architecture.Dto;
using System.Net;
using Tnf.Architecture.Web.Controllers;

namespace Tnf.Architecture.Web.Tests.Tests
{
    public class CountryControllerTests : AppTestBase
    {
        [Fact]
        public void Should_Resolve_Controller()
        {
            ServiceProvider.GetService<CountryController>().ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAll_Countries_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<PagedResultDto<CountryDto>>>(
                $"/{RouteConsts.Country}",
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.TotalCount, 5);
        }

        [Fact]
        public async Task GetAll_Countries_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Country}?maxResultCount=0",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, $"Invalid parameter: MaxResultCount");
        }

        [Fact]
        public async Task Get_Country_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<CountryDto>>(
                $"/{RouteConsts.Country}/1",
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Id, 1);
            Assert.Equal(response.Result.Name, "Brasil");
        }

        [Fact]
        public async Task Get_Country_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Country}/-1",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: id");
        }

        [Fact]
        public async Task Get_Country_When_Not_Exits_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Country}/99",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Post_Country_With_Success()
        {
            var countryDto = new CountryDto()
            {
                Id = 6,
                Name = "Canada"
            };

            // Act
            var response = await PostResponseAsObjectAsync<CountryDto, AjaxResponse<CountryDto>>(
                $"/{RouteConsts.Country}",
                countryDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Id, 6);
            Assert.Equal(response.Result.Name, "Canada");
        }

        [Fact]
        public async Task Post_Null_Country_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<CountryDto, AjaxResponse>(
                $"/{RouteConsts.Country}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            response.Result.ShouldBe("Invalid parameter: country");
        }

        [Fact]
        public async Task Put_Country_With_Success()
        {
            // Arrange
            var countryDto = new CountryDto()
            {
                Id = 4,
                Name = "Canada"
            };

            // Act
            var response = await PutResponseAsObjectAsync<CountryDto, AjaxResponse<CountryDto>>(
                $"/{RouteConsts.Country}/4",
                countryDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Id, 4);
            Assert.Equal(response.Result.Name, "Canada");
        }

        [Fact]
        public async Task Put_Null_Country_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CountryDto, AjaxResponse<string>>(
                $"/{RouteConsts.Country}/%20",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            response.Result.ShouldBe("Invalid parameter: id");
        }

        [Fact]
        public async Task Put_Country_When_Not_Exits_Return_Not_Found_Request()
        {
            // Arrange
            var countryDto = new CountryDto()
            {
                Id = 99,
                Name = "Canada"
            };

            // Act
            var response = await PutResponseAsObjectAsync<CountryDto, AjaxResponse<string>>(
                $"/{RouteConsts.Country}/99",
                countryDto,
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task Delete_Country_With_Success()
        {
            // Act
            await DeleteResponseAsObjectAsync<AjaxResponse>(
                $"/{RouteConsts.Country}/1",
                HttpStatusCode.OK
            );
        }

        [Fact]
        public async Task Delete_Country_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Country}/0",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Success.ShouldBeTrue();
            response.Result.ShouldBe("Invalid parameter: id");
        }
    }
}
