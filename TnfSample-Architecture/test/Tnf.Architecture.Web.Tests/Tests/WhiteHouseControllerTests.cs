using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Tnf.Web.Models;
using Tnf.Architecture.Dto;
using System.Net;
using Tnf.Architecture.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Tnf.Architecture.Dto.ValueObjects;
using System.Collections.Generic;
using Tnf.Dto;
using System.Linq;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Domain.WhiteHouse;

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
            var response = await GetResponseAsObjectAsync<AjaxResponse<PagingResponseDto<PresidentDto>>>(
                               "/api/white-house?pageSize=10",
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
                "/api/white-house?",
                HttpStatusCode.BadRequest
                );

            response.Success.ShouldBeTrue();
            response.Result.ShouldBe($"Invalid parameter: pageSize");
        }

        [Fact]
        public async Task Get_President_With_Sucess()
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
        public async Task Get_President_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                "/api/white-house/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: id");
        }

        [Fact]
        public async Task Get_President_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                               "/api/white-house/99",
                               HttpStatusCode.BadRequest
                           );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.Equal(response.Result, "President not found");
        }

        [Fact]
        public async Task Post_President_With_Success()
        {
            //Arrange
            var presidentDto = new PresidentDto()
            {
                Id = "7",
                Name = "Lula",
                ZipCode = new ZipCode("74125306")
            };

            var presidents = new List<PresidentDto>() { presidentDto };

            // Act
            var response = await PostResponseAsObjectAsync<List<PresidentDto>, AjaxResponse<DtoResponseBase<List<PresidentDto>>>>(
                "/api/white-house",
                presidents,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.True(response.Result.Data.Any(p => p.Name == "Lula"));
        }

        [Fact]
        public async Task Post_President_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<List<PresidentDto>, AjaxResponse<string>> (
                "/api/white-house",
                new List<PresidentDto>(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            response.Result.ShouldBe("Invalid parameter: presidents");
        }

        [Fact]
        public async Task Post_Empty_President_And_Return_Notifications()
        {
            // Act
            var response = await PostResponseAsObjectAsync<List<PresidentDto>, AjaxResponse<DtoResponseBase<List<PresidentDto>>>>(
                "/api/white-house",
                new List<PresidentDto>() { new PresidentDto() },
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.True(response.Result.Notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(response.Result.Notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_President_With_Success()
        {
            //Arrange
            var presidentDto = new PresidentDto("6", "Ronald Reagan", "85236417");

            // Act
            var response = await PutResponseAsObjectAsync<PresidentDto, AjaxResponse<PresidentDto>>(
                "/api/white-house/1",
                presidentDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Id, "6");
            Assert.Equal(response.Result.Name, "Ronald Reagan");
        }

        [Fact]
        public async Task Put_President_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<PresidentDto, AjaxResponse<string>>(
                "/api/white-house/%20",
                new PresidentDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            response.Result.ShouldBe("Invalid parameter: president");
        }

        [Fact]
        public async Task Delete_President_With_Success()
        {
            // Act
            await DeleteResponseAsObjectAsync<AjaxResponse>(
                "/api/white-house/1",
                HttpStatusCode.OK
            );
        }

        [Fact]
        public async Task Delete_President_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<AjaxResponse>(
                "/api/white-house/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Success.ShouldBeTrue();
            response.Result.ShouldBe("Invalid parameter: id");
        }
    }
}
