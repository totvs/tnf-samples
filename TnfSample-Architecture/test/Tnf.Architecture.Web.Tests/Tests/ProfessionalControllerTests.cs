using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Tnf.Architecture.Dto;
using System.Net;
using Tnf.Architecture.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.Enumerables;
using System;
using Tnf.Architecture.Dto.ValueObjects;
using System.Linq;
using Tnf.Architecture.Domain.Registration;
using System.Collections.Generic;
using Tnf.App.Dto.Response;
using Tnf.AspNetCore.Mvc.Response;

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
            var response = await GetResponseAsObjectAsync<ListDto<ProfessionalDto, ProfessionalKeysDto>>(
                               $"/{RouteConsts.Professional}?pageSize=5",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.Items.Count, 2);
        }

        [Fact]
        public async Task GetAll_Professionals_With_Paginated()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProfessionalDto, ProfessionalKeysDto>>(
                $"/{RouteConsts.Professional}?pageSize=1",
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Total, 2);
            Assert.Equal(response.HasNext, true);
            Assert.Equal(response.Items.Count, 1);
        }

        [Fact]
        public async Task GetAll_Professionals_Filtering_By_Name_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProfessionalDto, ProfessionalKeysDto>>(
                               $"{RouteConsts.Professional}?pageSize=10&name=Jos%C3%A9",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.Items.Count, 1);
            Assert.Equal(response.Items[0].Name, "José da Silva");
        }

        [Fact]
        public async Task GetAll_Professionals_Sorted_ASC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProfessionalDto, ProfessionalKeysDto>>(
                               $"{RouteConsts.Professional}?pageSize=10&order=name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.Items.Count, 2);
            Assert.Equal(response.Items[0].Name, "João da Silva");
        }

        [Fact]
        public async Task GetAll_Professionals_Sorted_DESC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProfessionalDto, ProfessionalKeysDto>>(
                               $"{RouteConsts.Professional}?pageSize=10&order=-name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.Items.Count, 2);
            Assert.Equal(response.Items[0].Name, "José da Silva");
        }


        [Fact]
        public async Task Get_Professional_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ProfessionalDto>(
                               $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.ProfessionalId, 1);
            Assert.Equal(response.Name, "João da Silva");
            Assert.NotNull(response.Address);
            Assert.Equal(response.Address.Street, "Rua Do Comercio");
            Assert.Equal(response.Address.Number, "123");
            Assert.Equal(response.Address.Complement, "APT 123");
            Assert.NotNull(response.Address.ZipCode);
            Assert.Equal(response.Address.ZipCode.Number, "99888777");
            Assert.NotNull(response.Specialties);
            Assert.Equal(response.Specialties.Count, 0);
        }

        [Fact]
        public async Task Get_Professional_Fields_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ProfessionalDto>(
                               $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637?fields=name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.ProfessionalId, 0);
            Assert.Equal(response.Name, "João da Silva");
            Assert.NotNull(response.Address);
            Assert.Equal(response.Address.Street, "");
            Assert.Equal(response.Address.Number, "");
            Assert.Equal(response.Address.Complement, null);
            Assert.NotNull(response.Address.ZipCode);
            Assert.Equal(response.Address.ZipCode.Number, "");
            Assert.NotNull(response.Specialties);
            Assert.Equal(response.Specialties.Count, 0);
        }

        [Fact]
        public async Task Get_Professional_Expand_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ProfessionalDto>(
                               $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637?expand=professionalSpecialties.specialty",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.ProfessionalId, 1);
            Assert.Equal(response.Name, "João da Silva");
            Assert.NotNull(response.Address);
            Assert.Equal(response.Address.Street, "Rua Do Comercio");
            Assert.Equal(response.Address.Number, "123");
            Assert.Equal(response.Address.Complement, "APT 123");
            Assert.NotNull(response.Address.ZipCode);
            Assert.Equal(response.Address.ZipCode.Number, "99888777");
            Assert.NotNull(response.Specialties);
            Assert.Equal(response.Specialties.Count, 2);
        }

        [Fact]
        public async Task Get_Professional_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                $"/{RouteConsts.Professional}/%20/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("GetProfessional");
            response.DetailedMessage.ShouldBe("GetProfessional");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Get_Professional_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                               $"{RouteConsts.Professional}/99/{Guid.NewGuid()}",
                               HttpStatusCode.NotFound
                           );

            // Assert
            response.Message.ShouldBe("GetProfessional");
            response.DetailedMessage.ShouldBe("GetProfessional");
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
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
            var response = await PostResponseAsObjectAsync<ProfessionalDto, ProfessionalDto>(
                $"/{RouteConsts.Professional}",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Name, "João da Silva");
            Assert.Equal(response.Address.Street, "Rua Do Comercio");
            Assert.Equal(response.Address.Number, "123");
            Assert.Equal(response.Address.Complement, "APT 123");
            Assert.Equal(response.Address.ZipCode.Number, "99888777");
        }

        [Fact]
        public async Task Post_Professional_Should_Be_Insert_And_Update_Item()
        {
            //Arrange
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
            var response = await PostResponseAsObjectAsync<ProfessionalDto, ProfessionalDto>(
                $"/{RouteConsts.Professional}",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            response.ProfessionalId.ShouldBe(2);

            response.Specialties.Clear();

            var updateParam = new ProfessionalDto()
            {
                Address = response.Address,
                Email = response.Email,
                Name = "Nome Alterado Teste",
                Phone = response.Phone
            };

            // Act
            await PutResponseAsObjectAsync<ProfessionalDto, ProfessionalDto>(
                $"/{RouteConsts.Professional}/{response.ProfessionalId}/{response.Code}",
                updateParam,
                HttpStatusCode.OK
            );

            response = await GetResponseAsObjectAsync<ProfessionalDto>(
                $"/{RouteConsts.Professional}/{response.ProfessionalId}/{response.Code}",
                HttpStatusCode.OK
            );

            //Assert
            response.Name.ShouldBe("Nome Alterado Teste");
        }

        [Fact]
        public async Task Post_Null_Professional_And_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<ProfessionalDto, ErrorResponse>(
                $"{RouteConsts.Professional}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PostProfessional");
            response.DetailedMessage.ShouldBe("PostProfessional");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Post_Empty_Professional_And_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<ProfessionalDto, ErrorResponse>(
                $"/{RouteConsts.Professional}",
                new ProfessionalDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PostProfessional");
            response.DetailedMessage.ShouldBe("PostProfessional");
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
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
            var response = await PutResponseAsObjectAsync<ProfessionalDto, ProfessionalDto>(
                $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                professionalDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.ProfessionalId, 1);
            Assert.Equal(response.Code.ToString(), "1b92f96f-6a71-4655-a0b9-93c5f6ad9637");
            Assert.Equal(response.Name, "João da Silva");
        }

        [Fact]
        public async Task Put_Professional_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProfessionalDto, ErrorResponse>(
                $"{RouteConsts.Professional}/0/{Guid.Empty}",
                new ProfessionalDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutProfessional");
            response.DetailedMessage.ShouldBe("PutProfessional");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Put_Null_Professional_And_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProfessionalDto, ErrorResponse>(
                $"{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutProfessional");
            response.DetailedMessage.ShouldBe("PutProfessional");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Put_Empty_Professional_And_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProfessionalDto, ErrorResponse>(
                $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                new ProfessionalDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutProfessional");
            response.DetailedMessage.ShouldBe("PutProfessional");
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalAddressComplementMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalAddressMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalAddressNumberMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalEmailMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalNameMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalPhoneMustHaveValue.ToString()));
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.ProfessionalZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_Professional_When_Not_Exists_Return_Not_Found()
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
            var response = await PutResponseAsObjectAsync<ProfessionalDto, ErrorResponse>(
                $"{RouteConsts.Professional}/99/{Guid.NewGuid()}",
                professionalDto,
                HttpStatusCode.NotFound
            );

            // Assert
            response.Message.ShouldBe("PutProfessional");
            response.DetailedMessage.ShouldBe("PutProfessional");
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }


        [Fact]
        public async Task Delete_Professional_With_Success()
        {
            // Act
            var response = await DeleteResponseAsStringAsync(
                $"/{RouteConsts.Professional}/1/1b92f96f-6a71-4655-a0b9-93c5f6ad9637",
                HttpStatusCode.OK
            );
        }

        [Fact]
        public async Task Delete_Professional_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"{RouteConsts.Professional}/%20/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("DeleteProfessional");
            response.DetailedMessage.ShouldBe("DeleteProfessional");
            Assert.True(response.Details.Any(n => n.Message == Error.InvalidParameter.ToString()));
        }

        [Fact]
        public async Task Delete_Professional_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"{RouteConsts.Professional}/99/{Guid.NewGuid()}",
                HttpStatusCode.NotFound
            );

            // Assert
            response.Message.ShouldBe("DeleteProfessional");
            response.DetailedMessage.ShouldBe("DeleteProfessional");
            Assert.True(response.Details.Any(a => a.Message == Professional.Error.CouldNotFindProfessional.ToString()));
        }
    }
}
