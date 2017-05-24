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
using Tnf.Dto.Response;
using Tnf.Dto.Interfaces;

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
            var response = await GetResponseAsObjectAsync<AjaxResponse<SuccessResponseListDto<SpecialtyDto>>>(
                               $"/{RouteConsts.Specialty}?pageSize=5",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Items.Count, 2);
        }

        [Fact]
        public async Task GetAll_Specialties_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Specialty}",
                HttpStatusCode.BadRequest
                );

            response.Success.ShouldBeTrue();
            response.Result.ShouldBe($"Invalid parameter: PageSize");
        }

        [Fact]
        public async Task GetAll_Specialties_Sorted_ASC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<SuccessResponseListDto<SpecialtyDto>>>(
                               $"{RouteConsts.Specialty}?pageSize=10&order=description",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Items.Count, 2);
            Assert.Equal(response.Result.Items[0].Description, "Cirurgia Geral");
        }

        [Fact]
        public async Task GetAll_Specialties_Sorted_DESC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<SuccessResponseListDto<SpecialtyDto>>>(
                               $"{RouteConsts.Specialty}?pageSize=10&order=-description",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Items.Count, 2);
            Assert.Equal(response.Result.Items[0].Description, "Cirurgia Vascular");
        }

        [Fact]
        public async Task Get_Specialty_With_Sucess()
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
        public async Task Get_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Specialty}/-1",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: id");
        }

        [Fact]
        public async Task Get_Specialty_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                               $"{RouteConsts.Specialty}/99",
                               HttpStatusCode.NotFound
                           );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Post_Specialty_With_Success()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 3,
                Description = "Cirurgia Torácica"
            };

            // Act
            var response = await PostResponseAsObjectAsync<SpecialtyDto, AjaxResponse<SpecialtyDto>>(
                $"/{RouteConsts.Specialty}",
                specialtyDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.True(response.Result.Success);
            Assert.Equal(response.Result.Description, "Cirurgia Torácica");
        }

        [Fact]
        public async Task Post_Null_Specialty_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<SpecialtyDto, AjaxResponse<string>>(
                $"{RouteConsts.Specialty}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            response.Result.ShouldBe("Invalid parameter: specialty");
        }

        [Fact]
        public async Task Post_Empty_Specialty_And_Return_Notifications()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto();

            // Act
            var response = await PostResponseAsObjectAsync<SpecialtyDto, AjaxResponse<ErrorResponseDto>>(
                $"/{RouteConsts.Specialty}",
                specialtyDto,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.True(response.Result.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_Specialty_With_Success()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Torácica"
            };

            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, AjaxResponse<SpecialtyDto>>(
                $"{RouteConsts.Specialty}/1",
                specialtyDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.True(response.Result.Success);
            Assert.Equal(response.Result.Id, 1);
            Assert.Equal(response.Result.Description, "Cirurgia Torácica");
        }

        [Fact]
        public async Task Put_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, AjaxResponse<string>>(
                $"{RouteConsts.Specialty}/-1",
                new SpecialtyDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            response.Result.ShouldBe("Invalid parameter: id");
        }

        [Fact]
        public async Task Put_Null_Specialty_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, AjaxResponse<string>>(
                $"/{RouteConsts.Specialty}/1",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: specialty");
        }

        [Fact]
        public async Task Put_Empty_Specialty_And_Return_Notifications()
        {
            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, AjaxResponse<ErrorResponseDto>>(
                $"{RouteConsts.Specialty}/1",
                new SpecialtyDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.True(response.Result.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_Specialty_When_Not_Exists_Return_Notifications()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 10,
                Description = "Cirurgia Torácica"
            };

            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, AjaxResponse<ErrorResponseDto>>(
                $"{RouteConsts.Specialty}/10",
                specialtyDto,
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.True(response.Result.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public async Task Delete_Specialty_With_Success()
        {
            // Act
            var responseDelete = await DeleteResponseAsObjectAsync<AjaxResponse<SuccessResponseDto>>(
                $"{RouteConsts.Specialty}/1",
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(responseDelete.Success);
            Assert.True(responseDelete.Result.Success);
        }

        [Fact]
        public async Task Delete_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<AjaxResponse<string>>(
                $"{RouteConsts.Specialty}/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Success.ShouldBeTrue();
            response.Result.ShouldBe("Invalid parameter: id");
        }

        [Fact]
        public async Task Delete_Specialty_When_Not_Exists_Return_Notifications()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<AjaxResponse<ErrorResponseDto>>(
                $"{RouteConsts.Specialty}/10",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.True(response.Result.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }
    }
}
