using System.Threading.Tasks;
using Tnf.Architecture.Web.Controllers;
using Xunit;
using Shouldly;
using Tnf.Web.Models;
using Tnf.Architecture.Dto;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto;
using System.Linq;
using Tnf.Architecture.Domain.Registration;


namespace Tnf.Architecture.Web.Tests.Tests
{
    public class SpecialtyControllerTests : AppTestBase
    {
        [Fact]
        public void Should_Resolve_Controller()
        {
            ServiceProvider.GetService<SpecialtyController>().ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAll_Specialties_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<PagingResponseDto<SpecialtyDto>>>(
                               $"/{RouteConsts.Specialty}?pageSize=10",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Data.Count, 2);
            response.Result.Notifications.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetAll_Specialties_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Specialty}?pageSize=0",
                HttpStatusCode.BadRequest
                );

            response.Success.ShouldBeTrue();
            response.Result.ShouldBe($"Invalid parameter: PageSize");
        }

        [Fact]
        public async Task Get_Specialties_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<SpecialtyDto>>(
                               $"/{RouteConsts.Specialty}/1",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.Equal(response.Result.Id, 1);
            Assert.Equal(response.Result.Description, "Cirurgia Vascular");
        }

        [Fact]
        public async Task Get_Specialties_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Specialty}/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: id");
        }

        [Fact]
        public async Task Post_Specialties_With_Success()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 3,
                Description = "Cirurgia Torácica"
            };

            // Act
            var response = await PostResponseAsObjectAsync<SpecialtyDto, AjaxResponse<DtoResponseBase<SpecialtyDto>>>(
                $"/{RouteConsts.Specialty}",
                specialtyDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Data.Description, "Cirurgia Torácica");
        }

        [Fact]
        public async Task Post_Empty_Professional_And_Return_Notifications()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto();

            // Act
            var response = await PostResponseAsObjectAsync<SpecialtyDto, AjaxResponse<DtoResponseBase<SpecialtyDto>>>(
                $"/{RouteConsts.Specialty}",
                specialtyDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.True(response.Result.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }
    }
}
