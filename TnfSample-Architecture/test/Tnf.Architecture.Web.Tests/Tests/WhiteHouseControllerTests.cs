using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Tnf.Architecture.Dto;
using System.Net;
using Tnf.Architecture.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.Enumerables;
using System.Linq;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Dto.Response;

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
            var response = await GetResponseAsObjectAsync<SuccessResponseListDto<PresidentDto>>(
                               $"{RouteConsts.WhiteHouse}?pageSize=5",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Items.Count, 5);
        }

        [Fact]
        public async Task GetAll_Presidents_Sorted_ASC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<SuccessResponseListDto<PresidentDto>>(
                               $"{RouteConsts.WhiteHouse}?pageSize=10&order=name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Items.Count, 6);
            Assert.Equal(response.Items[0].Name, "Abraham Lincoln");
        }

        [Fact]
        public async Task GetAll_Presidents_Sorted_DESC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<SuccessResponseListDto<PresidentDto>>(
                               $"{RouteConsts.WhiteHouse}?pageSize=10&order=-name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Items.Count, 6);
            Assert.Equal(response.Items[0].Name, "Thomas Jefferson");
        }

        [Fact]
        public async Task GetAll_Presidents_With_Invalid_Parameters()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponseDto>(
                $"{RouteConsts.WhiteHouse}",
                HttpStatusCode.BadRequest
                );

            // Assert
            Assert.False(response.Success);
            response.Message.ShouldBe("InvalidParameter");
            response.DetailedMessage.ShouldBe("InvalidParameter");
            Assert.True(response.Notifications.Any(n => n.Message == Error.InvalidParameterDynamic.ToString()));
        }

        [Fact]
        public async Task Get_President_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<PresidentDto>(
                               $"{RouteConsts.WhiteHouse}/1",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.Id == "1");
            Assert.NotNull(response.Name == "George Washington");
            Assert.NotNull(response.Address.Number == "12345678");
        }

        [Fact]
        public async Task Get_President_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponseDto>(
                $"{RouteConsts.WhiteHouse}/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            response.Message.ShouldBe("InvalidParameter");
            response.DetailedMessage.ShouldBe("InvalidParameter");
            Assert.True(response.Notifications.Any(n => n.Message == Error.InvalidParameterDynamic.ToString()));
        }

        [Fact]
        public async Task Get_President_When_Not_Exists_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponseDto>(
                               $"{RouteConsts.WhiteHouse}/99",
                               HttpStatusCode.NotFound
                           );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task Post_President_With_Success()
        {
            //Arrange
            var presidentDto = new PresidentDto()
            {
                Id = "7",
                Name = "Lula",
                Address = new Address("Rua de teste", "123", "APT 12", new ZipCode("74125306"))
            };

            // Act
            var response = await PostResponseAsObjectAsync<PresidentDto, PresidentDto>(
                $"{RouteConsts.WhiteHouse}",
                presidentDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Name, "Lula");
        }

        [Fact]
        public async Task Post_President_With_Invalid_Parameter_And_Return_Notifications()
        {
            // Act
            var response = await PostResponseAsObjectAsync<PresidentDto, ErrorResponseDto>(
                $"{RouteConsts.WhiteHouse}",
                new PresidentDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_President_With_Success()
        {
            //Arrange
            var presidentDto = new PresidentDto("6", "Ronald Reagan", new Address("Rua de teste", "123", "APT 12", new ZipCode("74125306")));

            // Act
            var response = await PutResponseAsObjectAsync<PresidentDto, PresidentDto>(
                $"{RouteConsts.WhiteHouse}/6",
                presidentDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.Id, "6");
            Assert.Equal(response.Name, "Ronald Reagan");
        }

        [Fact]
        public async Task Put_President_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<PresidentDto, ErrorResponseDto>(
                $"{RouteConsts.WhiteHouse}/%20",
                new PresidentDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            response.Message.ShouldBe("InvalidParameter");
            response.DetailedMessage.ShouldBe("InvalidParameter");
            Assert.True(response.Notifications.Any(n => n.Message == Error.InvalidParameterDynamic.ToString()));
        }

        [Fact]
        public async Task Put_Empty_President_And_Return_Notifications()
        {
            // Act
            var response = await PutResponseAsObjectAsync<PresidentDto, ErrorResponseDto>(
                $"{RouteConsts.WhiteHouse}/6",
                new PresidentDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.PresidentNameMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.PresidentAddressComplementMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.PresidentAddressMustHaveValue.ToString()));
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
        }

        [Fact]
        public async Task Put_President_When_Not_Exists_Return_Notifications()
        {
            //Arrange
            var presidentDto = new PresidentDto("99", "Ronald Reagan", new Address("Rua de teste", "123", "APT 12", new ZipCode("74125306")));

            // Act
            var response = await PutResponseAsObjectAsync<PresidentDto, ErrorResponseDto>(
                $"{RouteConsts.WhiteHouse}/99",
                presidentDto,
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }

        [Fact]
        public async Task Delete_President_With_Success()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<SuccessResponseDto>(
                $"{RouteConsts.WhiteHouse}/1",
                HttpStatusCode.OK
            );

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task Delete_President_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponseDto>(
                $"{RouteConsts.WhiteHouse}/%20",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.Success);
            response.Message.ShouldBe("InvalidParameter");
            response.DetailedMessage.ShouldBe("InvalidParameter");
            Assert.True(response.Notifications.Any(n => n.Message == Error.InvalidParameterDynamic.ToString()));
        }

        [Fact]
        public async Task Delete_President_When_Not_Exists_Return_Notifications()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponseDto>(
                $"{RouteConsts.WhiteHouse}/99",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.False(response.Success);
            Assert.True(response.Notifications.Any(a => a.Message == President.Error.CouldNotFindPresident.ToString()));
        }
    }
}
