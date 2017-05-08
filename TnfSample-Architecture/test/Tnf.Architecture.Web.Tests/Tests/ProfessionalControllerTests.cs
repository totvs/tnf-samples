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
using Tnf.AutoMapper;

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
        public async Task GetAll_Professional_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<PagingResponseDto<ProfessionalDto>>>(
                               $"/{RouteConsts.Professional}?pageSize=10",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Data.Count, 2);
            response.Result.Notifications.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetAll_Professional_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<AjaxResponse<string>>(
                $"/{RouteConsts.Professional}?pageSize=0",
                HttpStatusCode.BadRequest
                );

            response.Success.ShouldBeTrue();
            response.Result.ShouldBe($"Invalid parameter: PageSize");
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
        public async Task Post_Professional_With_Success()
        {
            //Arrange
            var professionalDto = new ProfessionalCreateDto()
            {
                ProfessionalId = 1,
                Address = new Address("Rua do comercio", "123", "APT 123", new ZipCode("99888777")),
                Email = "email@email.com",
                Name = "João da Silva",
                Phone = "55998765432"
            };

            // Act
            var response = await PostResponseAsObjectAsync<ProfessionalCreateDto, AjaxResponse<DtoResponseBase<ProfessionalDto>>>(
                $"/{RouteConsts.Professional}",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Result.Data.Name, "João da Silva");
            Assert.Equal(response.Result.Data.Address.Street, "Rua Do Comercio");
            Assert.Equal(response.Result.Data.Address.Number, "123");
            Assert.Equal(response.Result.Data.Address.Complement, "APT 123");
            Assert.Equal(response.Result.Data.Address.ZipCode.Number, "99888777");
        }

        [Fact]
        public async Task Post_Empty_Professional_And_Return_Notifications()
        {
            //Arrange
            var professionalDto = new ProfessionalCreateDto();

            // Act
            var response = await PostResponseAsObjectAsync<ProfessionalCreateDto, AjaxResponse<DtoResponseBase<ProfessionalDto>>>(
                $"/{RouteConsts.Professional}",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.False(response.Result.Success);
            Assert.True(response.Result.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(response.Result.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(response.Result.Notifications.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(response.Result.Notifications.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(response.Result.Notifications.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(response.Result.Notifications.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(response.Result.Notifications.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Post_Professional_Should_Be_Insert_And_Update_Item()
        {
            var professionalDto = new ProfessionalCreateDto()
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
            var response = await PostResponseAsObjectAsync<ProfessionalCreateDto, AjaxResponse<DtoResponseBase<ProfessionalDto>>>(
                $"/{RouteConsts.Professional}",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            response.Success.ShouldBeTrue();
            response.Result.Data.ProfessionalId.ShouldBe(2);

            response.Result.Data.Name = "Rua alterada de teste";

            response.Result.Data.Specialties.Clear();

            var updateParam = new ProfessionalUpdateDto()
            {
                Address = response.Result.Data.Address,
                Email = response.Result.Data.Email,
                Name = response.Result.Data.Name,
                Phone = response.Result.Data.Phone
            };

            // Act
            response = await PutResponseAsObjectAsync<ProfessionalUpdateDto, AjaxResponse<DtoResponseBase<ProfessionalDto>>>(
                $"/{RouteConsts.Professional}/2/{response.Result.Data.Code}",
                updateParam,
                HttpStatusCode.OK
            );

            //Assert
            response.Success.ShouldBeTrue();
            response.Result.Data.Name.ShouldBe("Rua alterada de teste");
        }
    }
}
