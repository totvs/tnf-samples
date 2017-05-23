using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Tnf.Web.Models;
using Tnf.Architecture.Dto;
using System.Net;
using Tnf.Architecture.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Tnf.Architecture.Dto.Registration;
using System;
using Tnf.Dto;
using Tnf.Architecture.Dto.ValueObjects;
using System.Linq;
using Tnf.Architecture.Domain.Registration;
using System.Collections.Generic;
using Tnf.Dto.Response;
using Tnf.Dto.Interfaces;

namespace Tnf.Architecture.Web.Tests.Tests
{
    public class ProfessionalControllerTests : AppTestBase
    {
        [Fact]
        public void Should_Resolve_Controller()
        {
            ServiceProvider.GetService<ProfessionalController>().ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAll_Professionals_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<SuccessResponseListDto<ProfessionalDto>>>(
                               $"/{RouteConsts.Professional}?pageSize=5",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Items.Count, 2);
        }

        [Fact]
        public async Task GetAll_Professionals_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Professional}",
                HttpStatusCode.BadRequest
                );

            response.Success.ShouldBeTrue();
            response.Result.ShouldBe($"Invalid parameter: PageSize");
        }

        [Fact]
        public async Task GetAll_Professionals_Sorted_ASC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<SuccessResponseListDto<ProfessionalDto>>>(
                               $"{RouteConsts.Professional}?pageSize=10&order=name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Items.Count, 2);
            Assert.Equal(response.Result.Items[0].Name, "João da Silva");
        }

        [Fact]
        public async Task GetAll_Professionals_Sorted_DESC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<SuccessResponseListDto<ProfessionalDto>>>(
                               $"{RouteConsts.Professional}?pageSize=10&order=-name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Items.Count, 2);
            Assert.Equal(response.Result.Items[0].Name, "José da Silva");
        }

        [Fact]
        public async Task Get_Professional_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<ProfessionalDto>>(
                               $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.Equal(response.Result.ProfessionalId, 1);
            Assert.Equal(response.Result.Name, "João da Silva");
            Assert.Equal(response.Result.Address.Street, "Rua Do Comercio");
            Assert.Equal(response.Result.Address.Number, "123");
            Assert.Equal(response.Result.Address.Complement, "APT 123");
            Assert.Equal(response.Result.Address.ZipCode.Number, "99888777");
        }

        [Fact]
        public async Task Get_Professional_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Professional}/%20/23",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: professionalId");

            // Act
            response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Professional}/1/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: code");
        }

        [Fact]
        public async Task Get_Professional_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                               $"{RouteConsts.Professional}/99/{Guid.NewGuid()}",
                               HttpStatusCode.NotFound
                           );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Post_Professional_With_Success()
        {
            //Arrange
            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 1,
                Address = new Address("Rua do comercio", "123", "APT 123", new ZipCode("99888777")),
                Email = "email@email.com",
                Name = "João da Silva",
                Phone = "55998765432"
            };

            // Act
            var response = await PostResponseAsObjectAsync<ProfessionalDto, AjaxResponse<IResponseDto>>(
                $"/{RouteConsts.Professional}",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.True(response.Result.Success);
            Assert.IsType(typeof(ProfessionalDto), response.Result);
            var professional = response.Result as ProfessionalDto;
            Assert.Equal(professional.Name, "João da Silva");
            Assert.Equal(professional.Address.Street, "Rua Do Comercio");
            Assert.Equal(professional.Address.Number, "123");
            Assert.Equal(professional.Address.Complement, "APT 123");
            Assert.Equal(professional.Address.ZipCode.Number, "99888777");
        }

        [Fact]
        public async Task Post_Null_Professional_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<ProfessionalDto, AjaxResponse<string>>(
                $"{RouteConsts.Professional}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            response.Result.ShouldBe("Invalid parameter: professional");
        }

        [Fact]
        public async Task Post_Empty_Professional_And_Return_Notifications()
        {
            //Arrange
            var professionalDto = new ProfessionalDto();

            // Act
            var response = await PostResponseAsObjectAsync<ProfessionalDto, AjaxResponse<IResponseDto>>(
                $"/{RouteConsts.Professional}",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.IsType(typeof(ErrorResponseDto), response.Result);
            var errorResponse = response.Result as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Post_Professional_Should_Be_Insert_And_Update_Item()
        {
            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 2,
                Address = new Address("Rua teste", "98765", "APT 9876", new ZipCode("23156478")),
                Email = "email1234@email.com",
                Name = "Jose da Silva",
                Phone = "58962348",
                Specialties = new List<SpecialtyDto>()
                {
                    new SpecialtyDto() { Id = 1, Description = "Anestesiologia" }
                }
            };

            // Act
            var response = await PostResponseAsObjectAsync<ProfessionalDto, AjaxResponse<IResponseDto>>(
                $"/{RouteConsts.Professional}",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            response.Success.ShouldBeTrue();
            response.Result.Success.ShouldBeTrue();
            Assert.IsType(typeof(ProfessionalDto), response.Result);
            var professional = response.Result as ProfessionalDto;

            professional.ProfessionalId.ShouldBe(2);

            professional.Specialties.Clear();

            var updateParam = new ProfessionalDto()
            {
                Address = professional.Address,
                Email = professional.Email,
                Name = "Nome Alterado Teste",
                Phone = professional.Phone
            };

            // Act
            response = await PutResponseAsObjectAsync<ProfessionalDto, AjaxResponse<IResponseDto>>(
                $"/{RouteConsts.Professional}/2/{professional.Code}",
                updateParam,
                HttpStatusCode.OK
            );

            //Assert
            response.Success.ShouldBeTrue();
            Assert.IsType(typeof(ProfessionalDto), response.Result);
            professional = response.Result as ProfessionalDto;
            professional.Name.ShouldBe("Nome Alterado Teste");
        }

        [Fact]
        public async Task Put_Professional_With_Success()
        {
            //Arrange
            var professionalDto = new ProfessionalDto()
            {
                Address = new Address("Rua do comercio", "123", "APT 123", new ZipCode("99888777")),
                Email = "email@email.com",
                Name = "João da Silva",
                Phone = "55998765432"
            };

            // Act
            var response = await PutResponseAsObjectAsync<ProfessionalDto, AjaxResponse<IResponseDto>>(
                $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.IsType(typeof(ProfessionalDto), response.Result);
            var professional = response.Result as ProfessionalDto;
            Assert.Equal(professional.ProfessionalId, 1);
            Assert.Equal(professional.Code.ToString(), "1b92f96f-6a71-4655-a0b9-93c5f6ad9637");
            Assert.Equal(professional.Name, "João da Silva");
        }

        [Fact]
        public async Task Put_Professional_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProfessionalDto, AjaxResponse<string>>(
                $"{RouteConsts.Professional}/0/{Guid.Empty}",
                new ProfessionalDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: professionalId");

            // Act
            response = await PutResponseAsObjectAsync<ProfessionalDto, AjaxResponse<string>>(
                $"/{RouteConsts.Professional}/1/{Guid.Empty}",
                new ProfessionalDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: code");
        }

        [Fact]
        public async Task Put_Null_Professional_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProfessionalDto, AjaxResponse<string>>(
                $"/{RouteConsts.Professional}/1/{Guid.NewGuid()}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result, "Invalid parameter: professional");
        }

        [Fact]
        public async Task Put_Empty_Professional_And_Return_Notifications()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProfessionalDto, AjaxResponse<IResponseDto>>(
                $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                new ProfessionalDto(),
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.IsType(typeof(ErrorResponseDto), response.Result);
            var errorResponse = response.Result as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_Professional_When_Not_Exists_Return_Notifications()
        {
            //Arrange
            var professionalDto = new ProfessionalDto()
            {
                Address = new Address("Rua do comercio", "123", "APT 123", new ZipCode("99888777")),
                Email = "email@email.com",
                Name = "João da Silva",
                Phone = "55998765432"
            };

            // Act
            var response = await PutResponseAsObjectAsync<ProfessionalDto, AjaxResponse<IResponseDto>>(
                $"{RouteConsts.Professional}/99/{Guid.NewGuid()}",
                professionalDto,
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.IsType(typeof(ErrorResponseDto), response.Result);
            var errorResponse = response.Result as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }

        [Fact]
        public async Task Delete_Professional_With_Success()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<AjaxResponse<IResponseDto>>(
                $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.True(response.Result.Success);
        }

        [Fact]
        public async Task Delete_Professional_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<AjaxResponse<string>>(
                $"{RouteConsts.Professional}/%20/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Success.ShouldBeTrue();
            response.Result.ShouldBe("Invalid parameter: professionalId");
        }

        [Fact]
        public async Task Delete_Professional_When_Not_Exists_Return_Notifications()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<AjaxResponse<IResponseDto>>(
                $"{RouteConsts.Professional}/99/{Guid.NewGuid()}",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.IsType(typeof(ErrorResponseDto), response.Result);
            var errorResponse = response.Result as ErrorResponseDto;
            Assert.True(errorResponse.Notifications.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }
    }
}
