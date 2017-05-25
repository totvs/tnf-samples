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
            var response = await GetResponseAsObjectAsync<SuccessResponseListDto<SpecialtyDto>>(
                               $"/{RouteConsts.Specialty}?pageSize=5",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Items.Count, 2);
        }

        [Fact]
        public async Task GetAll_Specialties_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponseDto>(
                $"/{RouteConsts.Specialty}",
                HttpStatusCode.BadRequest
                );

            // Assert
            Assert.False(response.Success);
            response.Message.ShouldBe("Invalid parameter");
            response.DetailedMessage.ShouldBe("Invalid parameter: PageSize");
        }

        [Fact]
        public async Task GetAll_Specialties_Sorted_ASC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<SuccessResponseListDto<SpecialtyDto>>(
                               $"{RouteConsts.Specialty}?pageSize=10&order=description",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Items.Count, 2);
            Assert.Equal(response.Items[0].Description, "Cirurgia Geral");
        }

        [Fact]
        public async Task GetAll_Specialties_Sorted_DESC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<SuccessResponseListDto<SpecialtyDto>>(
                               $"{RouteConsts.Specialty}?pageSize=10&order=-description",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Items.Count, 2);
            Assert.Equal(response.Items[0].Description, "Cirurgia Vascular");
        }

        [Fact]
        public async Task Get_Specialty_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<SpecialtyDto>(
                               $"/{RouteConsts.Specialty}/1",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Id, 1);
            Assert.Equal(response.Description, "Cirurgia Vascular");
        }

        [Fact]
        public async Task Get_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponseDto>(
                $"/{RouteConsts.Specialty}/-1",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            response.Message.ShouldBe("Invalid parameter");
            response.DetailedMessage.ShouldBe("Invalid parameter: id");
        }

        [Fact]
        public async Task Get_Specialty_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponseDto>(
                               $"{RouteConsts.Specialty}/99",
                               HttpStatusCode.NotFound
                           );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
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
            var response = await PostResponseAsObjectAsync<SpecialtyDto, SpecialtyDto>(
                $"/{RouteConsts.Specialty}",
                specialtyDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Description, "Cirurgia Torácica");
        }

        [Fact]
        public async Task Post_Empty_Specialty_And_Return_Notifications()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto();

            // Act
            var response = await PostResponseAsObjectAsync<SpecialtyDto, ErrorResponseDto>(
                $"/{RouteConsts.Specialty}",
                specialtyDto,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
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
            var response = await PutResponseAsObjectAsync<SpecialtyDto, SpecialtyDto>(
                $"{RouteConsts.Specialty}/1",
                specialtyDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Id, 1);
            Assert.Equal(response.Description, "Cirurgia Torácica");
        }

        [Fact]
        public async Task Put_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, ErrorResponseDto>(
                $"{RouteConsts.Specialty}/-1",
                new SpecialtyDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            response.Message.ShouldBe("Invalid parameter");
            response.DetailedMessage.ShouldBe("Invalid parameter: id");
        }

        [Fact]
        public async Task Put_Empty_Specialty_And_Return_Notifications()
        {
            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, ErrorResponseDto>(
                $"{RouteConsts.Specialty}/1",
                new SpecialtyDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
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
            var response = await PutResponseAsObjectAsync<SpecialtyDto, ErrorResponseDto>(
                $"{RouteConsts.Specialty}/10",
                specialtyDto,
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }

        [Fact]
        public async Task Delete_Specialty_With_Success()
        {
            // Act
            var responseDelete = await DeleteResponseAsObjectAsync<SuccessResponseDto>(
                $"{RouteConsts.Specialty}/1",
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(responseDelete.Success);
        }

        [Fact]
        public async Task Delete_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponseDto>(
                $"{RouteConsts.Specialty}/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            response.Message.ShouldBe("Invalid parameter");
            response.DetailedMessage.ShouldBe("Invalid parameter: id");
        }

        [Fact]
        public async Task Delete_Specialty_When_Not_Exists_Return_Notifications()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponseDto>(
                $"{RouteConsts.Specialty}/10",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }
    }
}
