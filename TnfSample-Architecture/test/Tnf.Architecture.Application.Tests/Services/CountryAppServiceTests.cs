using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Tnf.Application.Services.Dto;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Application.Interfaces;

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
        public async Task Should_Get_All_Countries_With_Success()
        {
            var requestDto = new PagedAndSortedResultRequestDto();

            var result = await _countryAppService.GetAll(requestDto);

            result.Items.Count.ShouldBe(5);

            requestDto.MaxResultCount = 4;

            result = await _countryAppService.GetAll(requestDto);

            result.Items.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Should_Get_Country_With_Success()
        {
            var result = await _countryAppService.Get(new EntityDto<int>(1));

            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Brasil");
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

            result = await _countryAppService.Update(new CountryDto()
            {
                Id = result.Id,
                Name = "Canada"
            });

            result.Name.ShouldBe("Canada");
        }

        [Fact]
        public async Task Should_Delete_Country_With_Success()
        {
            var result = await _countryAppService.Create(new CountryDto()
            {
                Id = 6,
                Name = "Mexico"
            });

            await _countryAppService.Delete(new EntityDto<int>(result.Id));

            var pagedResult = await _countryAppService.GetAll(new PagedAndSortedResultRequestDto());
            pagedResult.Items.Count.ShouldBe(5);
        }
    }
}