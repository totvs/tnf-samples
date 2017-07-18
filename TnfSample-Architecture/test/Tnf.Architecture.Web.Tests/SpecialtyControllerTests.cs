using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Enumerables;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Web.Controllers;
using Tnf.App.AspNetCore.Mvc.Response;
using Xunit;

namespace Tnf.Architecture.Web.Tests
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
            var response = await GetResponseAsObjectAsync<ListDto<SpecialtyDto, int>>(
                               $"/{RouteConsts.Specialty}?pageSize=5"
                           );

            // Assert
            Assert.Equal(response.Items.Count, 2);
        }

        [Fact]
        public async Task GetAll_Specialties_Filtering_By_Description_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<SpecialtyDto, int>>(
                               $"{RouteConsts.Specialty}?pageSize=10&description=Geral"
                           );

            // Assert
            Assert.Equal(response.Items.Count, 1);
            Assert.Equal(response.Items[0].Description, "Cirurgia Geral");
        }

        [Fact]
        public async Task GetAll_Specialties_Sorted_ASC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<SpecialtyDto, int>>(
                               $"{RouteConsts.Specialty}?pageSize=10&order=description"
                           );

            // Assert
            Assert.Equal(response.Items.Count, 2);
            Assert.Equal(response.Items[0].Description, "Cirurgia Geral");
        }

        [Fact]
        public async Task GetAll_Specialties_Sorted_DESC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<SpecialtyDto, int>>(
                               $"{RouteConsts.Specialty}?pageSize=10&order=-description"
                           );

            // Assert
            Assert.Equal(response.Items.Count, 2);
            Assert.Equal(response.Items[0].Description, "Cirurgia Vascular");
        }


        [Fact]
        public async Task Get_Specialty_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<SpecialtyDto>(
                               $"/{RouteConsts.Specialty}/1"
                           );

            // Assert
            Assert.Equal(response.Id, 1);
            Assert.Equal(response.Description, "Cirurgia Vascular");
        }

        [Fact]
        public async Task Get_Specialty_Fields_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<SpecialtyDto>(
                               $"/{RouteConsts.Specialty}/1?fields=description"
                           );

            // Assert
            Assert.Equal(response.Id, 0);
            Assert.Equal(response.Description, "Cirurgia Vascular");
        }

        [Fact]
        public async Task Get_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                $"/{RouteConsts.Specialty}/-1",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("GetSpecialty");
            response.DetailedMessage.ShouldBe("GetSpecialty");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Get_Specialty_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                               $"{RouteConsts.Specialty}/99",
                               HttpStatusCode.NotFound
                           );

            // Assert
            response.Message.ShouldBe("GetSpecialty");
            response.DetailedMessage.ShouldBe("GetSpecialty");
            Assert.True(response.Details.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
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
                specialtyDto
            );

            // Assert
            Assert.Equal(response.Description, "Cirurgia Torácica");
        }

        [Fact]
        public async Task Post_Specialty_Should_Be_Insert_And_Update_Item()
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
                specialtyDto
            );

            // Assert
            response.Id.ShouldBe(3);

            var updateParam = new SpecialtyDto()
            {
                Description = "Descrição Alterada Teste"
            };

            // Act
            await PutResponseAsObjectAsync<SpecialtyDto, SpecialtyDto>(
                $"/{RouteConsts.Specialty}/{response.Id}",
                updateParam
            );

            response = await GetResponseAsObjectAsync<SpecialtyDto>(
                $"/{RouteConsts.Specialty}/{response.Id}"
            );

            //Assert
            response.Description.ShouldBe("Descrição Alterada Teste");
        }

        [Fact]
        public async Task Post_Null_Specialty_And_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<SpecialtyDto, ErrorResponse>(
                $"{RouteConsts.Specialty}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PostSpecialty");
            response.DetailedMessage.ShouldBe("PostSpecialty");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Post_Empty_Specialty_And_Return_Bad_Request()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto();

            // Act
            var response = await PostResponseAsObjectAsync<SpecialtyDto, ErrorResponse>(
                $"/{RouteConsts.Specialty}",
                specialtyDto,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PostSpecialty");
            response.DetailedMessage.ShouldBe("PostSpecialty");
            Assert.True(response.Details.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
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
            await PutResponseAsObjectAsync<SpecialtyDto, SpecialtyDto>(
                $"{RouteConsts.Specialty}/1",
                specialtyDto
            );

            var response = await GetResponseAsObjectAsync<SpecialtyDto>(
                $"{RouteConsts.Specialty}/1"
            );

            // Assert
            Assert.Equal(response.Id, 1);
            Assert.Equal(response.Description, "Cirurgia Torácica");
        }

        [Fact]
        public async Task Put_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, ErrorResponse>(
                $"{RouteConsts.Specialty}/-1",
                new SpecialtyDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutSpecialty");
            response.DetailedMessage.ShouldBe("PutSpecialty");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Put_Null_Specialty_And_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, ErrorResponse>(
                $"{RouteConsts.Specialty}/1",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutSpecialty");
            response.DetailedMessage.ShouldBe("PutSpecialty");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Put_Empty_Specialty_And_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, ErrorResponse>(
                $"{RouteConsts.Specialty}/1",
                new SpecialtyDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutSpecialty");
            response.DetailedMessage.ShouldBe("PutSpecialty");
            Assert.True(response.Details.Any(a => a.Message == Specialty.Error.SpecialtyDescriptionMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_Specialty_When_Not_Exists_Return_Not_Found()
        {
            //Arrange
            var specialtyDto = new SpecialtyDto()
            {
                Id = 10,
                Description = "Cirurgia Torácica"
            };

            // Act
            var response = await PutResponseAsObjectAsync<SpecialtyDto, ErrorResponse>(
                $"{RouteConsts.Specialty}/10",
                specialtyDto,
                HttpStatusCode.NotFound
            );

            // Assert
            response.Message.ShouldBe("PutSpecialty");
            response.DetailedMessage.ShouldBe("PutSpecialty");
            Assert.True(response.Details.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }


        [Fact]
        public async Task Delete_Specialty_With_Success()
        {
            // Act
            await DeleteResponseAsStringAsync(
                $"{RouteConsts.Specialty}/1"
            );
        }

        [Fact]
        public async Task Delete_Specialty_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"{RouteConsts.Specialty}/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("DeleteSpecialty");
            response.DetailedMessage.ShouldBe("DeleteSpecialty");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Delete_Specialty_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"{RouteConsts.Specialty}/10",
                HttpStatusCode.NotFound
            );

            // Assert
            response.Message.ShouldBe("DeleteSpecialty");
            response.DetailedMessage.ShouldBe("DeleteSpecialty");
            Assert.True(response.Details.Any(a => a.Message == Specialty.Error.CouldNotFindSpecialty.ToString()));
        }
    }
}
