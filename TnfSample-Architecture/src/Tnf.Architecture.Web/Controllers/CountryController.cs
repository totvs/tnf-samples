using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.Country)]
    public class CountryController : ArchitectureControllerBase
    {
        private readonly ICountryAppService _countryAppService;

        public CountryController(ICountryAppService countryAppService)
        {
            _countryAppService = countryAppService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery]GetAllCountriesDto requestDto)
        {
            var response = await _countryAppService.GetAll(requestDto);

            return CreateResponse<CountryDto>()
                        .FromErrorEnum(CountryDto.Error.GetAllCountry)
                        .WithMessage(AppConsts.LocalizationSourceName, CountryDto.Error.GetAllCountry)
                        .WithDto(response)
                        .Build();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery]RequestDto request)
        {
            var response = await _countryAppService.Get(request.WithId(id));

            return CreateResponse<CountryDto>()
                        .FromErrorEnum(CountryDto.Error.GetCountry)
                        .WithMessage(AppConsts.LocalizationSourceName, CountryDto.Error.GetCountry)
                        .WithDto(response)
                        .Build();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CountryDto country)
        {
            var response = await _countryAppService.Create(country);

            return CreateResponse<CountryDto>()
                        .FromErrorEnum(CountryDto.Error.PostCountry)
                        .WithMessage(AppConsts.LocalizationSourceName, CountryDto.Error.PostCountry)
                        .WithDto(response)
                        .Build();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CountryDto country)
        {
            var response = await _countryAppService.Update(id, country);

            return CreateResponse<CountryDto>()
                        .FromErrorEnum(CountryDto.Error.PutCountry)
                        .WithMessage(AppConsts.LocalizationSourceName, CountryDto.Error.PutCountry)
                        .WithDto(response)
                        .Build();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _countryAppService.Delete(id);

            return CreateResponse<CountryDto>()
                        .FromErrorEnum(CountryDto.Error.DeleteCountry)
                        .WithMessage(AppConsts.LocalizationSourceName, CountryDto.Error.DeleteCountry)
                        .Build();
        }
    }
}
