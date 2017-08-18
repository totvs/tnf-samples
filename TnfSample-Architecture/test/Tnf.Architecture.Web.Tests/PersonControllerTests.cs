using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tnf.App.Application.Enums;
using Tnf.App.AspNetCore.Mvc.Response;
using Tnf.App.Domain.Enums;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Common;
using Tnf.Architecture.Common.Enumerables;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Web.Controllers;
using Xunit;

namespace Tnf.Architecture.Web.Tests
{
    public class PersonControllerTests : AppTestBase
    {
        [Fact]
        public void Should_Resolve_Controller()
        {
            ServiceProvider.GetService<PersonController>().ShouldNotBeNull();
        }


        [Fact]
        public async Task GetAll_People_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PersonDto, int>>(
                               $"/{RouteConsts.Person}?pageSize=5"
                           );

            // Assert
            Assert.Equal(response.Items.Count, 5);
        }

        [Fact]
        public async Task GetAll_People_Filtering_By_Description_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PersonDto, int>>(
                               $"{RouteConsts.Person}?pageSize=10&name=Doe"
                           );

            // Assert
            Assert.Equal(response.Items.Count, 2);
            Assert.Equal(response.Items[0].Name, "John Doe");
        }

        [Fact]
        public async Task GetAll_People_Sorted_ASC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PersonDto, int>>(
                               $"{RouteConsts.Person}?pageSize=10&order=name"
                           );

            // Assert
            Assert.Equal(response.Items.Count, 5);
            Assert.Equal(response.Items[0].Name, "Abraham");
        }

        [Fact]
        public async Task GetAll_People_Sorted_DESC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PersonDto, int>>(
                               $"{RouteConsts.Person}?pageSize=10&order=-name"
                           );

            // Assert
            Assert.Equal(response.Items.Count, 5);
            Assert.Equal(response.Items[0].Name, "Mary Doe");
        }


        [Fact]
        public async Task Get_Person_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<PersonDto>(
                               $"/{RouteConsts.Person}/1"
                           );

            // Assert
            Assert.Equal(response.Id, 1);
            Assert.Equal(response.Name, "John Doe");
        }

        [Fact]
        public async Task Get_Person_Fields_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<PersonDto>(
                               $"/{RouteConsts.Person}/1?fields=name"
                           );

            // Assert
            Assert.Equal(response.Id, 0);
            Assert.Equal(response.Name, "John Doe");
        }

        [Fact]
        public async Task Get_Person_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                $"/{RouteConsts.Person}/0",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("GetPerson");
            response.DetailedMessage.ShouldBe("GetPerson");
            Assert.True(response.Details.Any(n => n.Message == AppApplicationErrors.AppApplicationErrorsInvalidId.ToString()));
        }

        [Fact]
        public async Task Get_Person_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                               $"{RouteConsts.Person}/99",
                               HttpStatusCode.NotFound
                           );

            // Assert
            response.Message.ShouldBe("GetPerson");
            response.DetailedMessage.ShouldBe("GetPerson");
            Assert.True(response.Details.Any(a => a.Message == AppDomainErrors.TnfAppDomainErrorOnGetCouldNotFind.ToString()));
        }


        [Fact]
        public async Task Post_Person_With_Success()
        {
            //Arrange
            var personDto = new PersonDto()
            {
                Id = 6,
                Name = "Smith"
            };

            // Act
            var response = await PostResponseAsObjectAsync<PersonDto, PersonDto>(
                $"/{RouteConsts.Person}",
                personDto
            );

            // Assert
            Assert.Equal(response.Name, "Smith");
        }

        [Fact]
        public async Task Post_Person_Should_Be_Insert_And_Update_Item()
        {
            //Arrange
            var personDto = new PersonDto()
            {
                Id = 6,
                Name = "Smith"
            };

            // Act
            var response = await PostResponseAsObjectAsync<PersonDto, PersonDto>(
                $"/{RouteConsts.Person}",
                personDto
            );

            // Assert
            response.Id.ShouldBe(6);

            var updateParam = new PersonDto()
            {
                Name = "Smith Test"
            };

            // Act
            await PutResponseAsObjectAsync<PersonDto, PersonDto>(
                $"/{RouteConsts.Person}/{response.Id}",
                updateParam
            );

            response = await GetResponseAsObjectAsync<PersonDto>(
                $"/{RouteConsts.Person}/{response.Id}"
            );

            //Assert
            response.Name.ShouldBe("Smith Test");
        }

        [Fact]
        public async Task Post_Null_Person_And_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<PersonDto, ErrorResponse>(
                $"{RouteConsts.Person}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PostPerson");
            response.DetailedMessage.ShouldBe("PostPerson");
            Assert.True(response.Details.Any(n => n.Message == AppApplicationErrors.AppApplicationErrorsInvalidDto.ToString()));
        }

        [Fact]
        public async Task Post_Empty_Person_And_Return_Bad_Request()
        {
            //Arrange
            var personDto = new PersonDto();

            // Act
            var response = await PostResponseAsObjectAsync<PersonDto, ErrorResponse>(
                $"/{RouteConsts.Person}",
                personDto,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PostPerson");
            response.DetailedMessage.ShouldBe("PostPerson");
            Assert.True(response.Details.Any(a => a.Message == Person.Error.PersonNameMustHaveValue.ToString()));
        }


        [Fact]
        public async Task Put_Person_With_Success()
        {
            //Arrange
            var personDto = new PersonDto()
            {
                Id = 1,
                Name = "Smith"
            };

            // Act
            await PutResponseAsObjectAsync<PersonDto, PersonDto>(
                $"{RouteConsts.Person}/1",
                personDto
            );

            var response = await GetResponseAsObjectAsync<PersonDto>(
                $"{RouteConsts.Person}/1"
            );

            // Assert
            Assert.Equal(response.Id, 1);
            Assert.Equal(response.Name, "Smith");
        }

        [Fact]
        public async Task Put_Person_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<PersonDto, ErrorResponse>(
                $"{RouteConsts.Person}/0",
                new PersonDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutPerson");
            response.DetailedMessage.ShouldBe("PutPerson");
            Assert.True(response.Details.Any(n => n.Message == AppApplicationErrors.AppApplicationErrorsInvalidId.ToString()));
        }

        [Fact]
        public async Task Put_Null_Person_And_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<PersonDto, ErrorResponse>(
                $"{RouteConsts.Person}/1",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutPerson");
            response.DetailedMessage.ShouldBe("PutPerson");
            Assert.True(response.Details.Any(n => n.Message == AppApplicationErrors.AppApplicationErrorsInvalidDto.ToString()));
        }

        [Fact]
        public async Task Put_Empty_Person_And_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<PersonDto, ErrorResponse>(
                $"{RouteConsts.Person}/1",
                new PersonDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutPerson");
            response.DetailedMessage.ShouldBe("PutPerson");
            Assert.True(response.Details.Any(a => a.Message == Person.Error.PersonNameMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_Person_When_Not_Exists_Return_Not_Found()
        {
            //Arrange
            var personDto = new PersonDto()
            {
                Id = 10,
                Name = "Smith"
            };

            // Act
            var response = await PutResponseAsObjectAsync<PersonDto, ErrorResponse>(
                $"{RouteConsts.Person}/10",
                personDto,
                HttpStatusCode.NotFound
            );

            // Assert
            response.Message.ShouldBe("PutPerson");
            response.DetailedMessage.ShouldBe("PutPerson");
            Assert.True(response.Details.Any(a => a.Message == AppDomainErrors.TnfAppDomainErrorOnUpdateCouldNotFind.ToString()));
        }


        [Fact]
        public async Task Delete_Person_With_Success()
        {
            // Act
            await DeleteResponseAsStringAsync(
                $"{RouteConsts.Person}/1"
            );
        }

        [Fact]
        public async Task Delete_Person_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"{RouteConsts.Person}/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("DeletePerson");
            response.DetailedMessage.ShouldBe("DeletePerson");
            Assert.True(response.Details.Any(n => n.Message == AppApplicationErrors.AppApplicationErrorsInvalidId.ToString()));
        }

        [Fact]
        public async Task Delete_Person_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"{RouteConsts.Person}/10",
                HttpStatusCode.NotFound
            );

            // Assert
            response.Message.ShouldBe("DeletePerson");
            response.DetailedMessage.ShouldBe("DeletePerson");
            Assert.True(response.Details.Any(a => a.Message == AppDomainErrors.TnfAppDomainErrorOnDeleteCouldNotFind.ToString()));
        }
    }
}
