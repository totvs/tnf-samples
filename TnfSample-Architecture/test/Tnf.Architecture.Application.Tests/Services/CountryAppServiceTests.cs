using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Tnf.App.Crud;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.Registration;
using Xunit;

namespace Tnf.Architecture.Application.Tests.Services
{
    public class CountryAppServiceTests : EfCoreAppTestBase
    {
        private readonly ICountryAppService _countryAppService;

        public CountryAppServiceTests()
        {
            _countryAppService = LocalIocManager.Resolve<ICountryAppService>();
        }

        [Fact]
        public void Service_Should_Not_Be_Null()
        {
            _countryAppService.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Get_All_Countries_With_Success()
        {
            var requestDto = new GetAllCountriesDto();

            var result = await _countryAppService.GetAll(requestDto);

            result.Items.Count.ShouldBe(5);

            requestDto.PageSize = 4;

            result = await _countryAppService.GetAll(requestDto);

            result.Items.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Should_Insert_Country_With_Success()
        {
            var result = await _countryAppService.Create(new CountryDto()
            {
                Id = 6,
                Name = "Mexico"
            });

            result.Name.ShouldBe("Mexico");
        }

        [Fact]
        public async Task Should_Insert_Null_Country_With_Error()
        {
            // Act
            var response = await _countryAppService.Create(null);

            // Assert
            Assert.Null(response);
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == CrudOperations.TnfAppCrudInvalidParameterError.ToString()));
        }

        [Fact]
        public async Task Should_Update_Country_With_Success()
        {
            var result = await _countryAppService.Create(new CountryDto()
            {
                Id = 6,
                Name = "Mexico"
            });

            result.Name.ShouldBe("Mexico");

            result = await _countryAppService.Update(result.Id, new CountryDto()
            {
                Name = "Canada"
            });

            result.Name.ShouldBe("Canada");
        }

        [Fact]
        public async Task Should_Update_Invalid_Id_With_Error()
        {
            // Act
            await _countryAppService.Update(0, new CountryDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.Equal(notifications.Count, 1);
            Assert.True(notifications.Any(n => n.Message == CrudOperations.TnfAppCrudInvalidParameterError.ToString()));
        }

        [Fact]
        public async Task Should_Update_Null_Country_With_Error()
        {
            // Act
            await _countryAppService.Update(1, null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.Equal(notifications.Count, 1);
            Assert.True(notifications.Any(n => n.Message == CrudOperations.TnfAppCrudInvalidParameterError.ToString()));
        }

        [Fact]
        public async Task Should_Get_Country_With_Success()
        {
            var result = await _countryAppService.Get(new RequestDto(1));

            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Brasil");
        }

        [Fact]
        public async Task Should_Get_Country_With_Error()
        {
            // Act
            var response = await _countryAppService.Get(new RequestDto(99));

            // Assert
            Assert.Null(response);
            Assert.True(LocalNotification.HasNotification());
            Assert.True(LocalNotification.GetAll().Any(a => a.Message == CrudOperations.TnfAppCrudOnGetCouldNotFind.ToString()));
        }

        [Fact]
        public async Task Should_Delete_Country_With_Success()
        {
            var requestDto = new GetAllCountriesDto();

            var result = await _countryAppService.GetAll(requestDto);

            result.Items.Count.ShouldBe(5);

            await _countryAppService.Delete(5);

            result = await _countryAppService.GetAll(requestDto);

            result.Items.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Should_Delete_Professional_With_Error()
        {
            // Act
            await _countryAppService.Delete(0);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == CrudOperations.TnfAppCrudInvalidParameterError.ToString()));
        }
    }
}