using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services.Dto;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;

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
        public async Task<IActionResult> Get(PagedAndSortedResultRequestDto requestDto)
        {
            if (requestDto == null)
                return BadRequest($"Invalid parameter: {nameof(requestDto)}");

            if (requestDto.MaxResultCount <= 0)
                return BadRequest($"Invalid parameter: {nameof(requestDto.MaxResultCount)}");

            var pagedResult = await _countryAppService.GetAll(requestDto);

            return Ok(pagedResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            try
            {
                var country = await _countryAppService.Get(new EntityDto<int>(id));
                return Ok(country);
            }
            catch (Exception)
            {
                return NotFound(L("CouldNotFindCountry"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CountryDto country)
        {
            if (country == null)
                return BadRequest($"Invalid parameter: {nameof(country)}");

            var result = await _countryAppService.Create(country);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CountryDto country)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            if (country == null)
                return BadRequest($"Invalid parameter: {nameof(country)}");

            country.Id = id;
            CountryDto result = null;

            try
            {
                result = await _countryAppService.Update(country);
            }
            catch (Exception)
            {
                //TODO: Estourar exceção quando não achar?
            }

            if (result == null)
                return NotFound(L("CouldNotFindCountry"));

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            await _countryAppService.Delete(new EntityDto<int>(id));
            return Ok();
        }
    }
}
