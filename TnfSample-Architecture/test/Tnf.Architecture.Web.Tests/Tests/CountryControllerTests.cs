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
            var response = await GetResponseAsObjectAsync<PagedResultDto<CountryDto>>(
                $"/{RouteConsts.Country}",
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.TotalCount, 5);
        }

        [Fact]
        public async Task GetAll_Countries_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<string>(
                $"/{RouteConsts.Country}?maxResultCount=0",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.Equal(response, $"Invalid parameter: MaxResultCount");
        }

        [Fact]
        public async Task Get_Country_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<CountryDto>(
                $"/{RouteConsts.Country}/1",
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Id, 1);
            Assert.Equal(response.Name, "Brasil");
        }

        [Fact]
        public async Task Get_Country_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<string>(
                $"/{RouteConsts.Country}/-1",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.Equal(response, "Invalid parameter: id");
        }

        [Fact]
        public async Task Get_Country_When_Not_Exits_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<string>(
                $"/{RouteConsts.Country}/99",
                HttpStatusCode.NotFound
            );

            // Assert
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
            var response = await PostResponseAsObjectAsync<CountryDto, CountryDto>(
                $"/{RouteConsts.Country}",
                countryDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Id, 6);
            Assert.Equal(response.Name, "Canada");
        }

        [Fact]
        public async Task Post_Null_Country_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<CountryDto, string>(
                $"/{RouteConsts.Country}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.ShouldBe("Invalid parameter: country");
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
            var response = await PutResponseAsObjectAsync<CountryDto, CountryDto>(
                $"/{RouteConsts.Country}/4",
                countryDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Id, 4);
            Assert.Equal(response.Name, "Canada");
        }

        [Fact]
        public async Task Put_Null_Country_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CountryDto, string>(
                $"/{RouteConsts.Country}/%20",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.ShouldBe("Invalid parameter: id");
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
            var response = await PutResponseAsObjectAsync<CountryDto, string>(
                $"/{RouteConsts.Country}/99",
                countryDto,
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Delete_Country_With_Success()
        {
            // Act
            await DeleteResponseAsObjectAsync<string>(
                $"/{RouteConsts.Country}/1",
                HttpStatusCode.OK
            );
        }

        [Fact]
        public async Task Delete_Country_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<string>(
                $"/{RouteConsts.Country}/0",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.ShouldBe("Invalid parameter: id");
        }
    }
}
