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
            Assert.NotNull(response.Result.ProfessionalId == 1);
            Assert.NotNull(response.Result.Name == "João da Silva");
            Assert.NotNull(response.Result.ZipCode.Number == "99888777");
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
                Address = "Rua do comercio",
                AddressNumber = "123",
                AddressComplement = "APT 123",
                Email = "email@email.com",
                Name = "João da Silva",
                Phone = "55998765432",
                ZipCode = new ZipCode("99888777")
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
        }
    }
}
