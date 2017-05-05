using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services.Dto;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Runtime.Validation;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.Country)]
    [DisableValidation]
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
                var result = await _countryAppService.Get(new EntityDto<int>(id));
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound("Country not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CountryDto dto)
        {
            if (dto == null)
                return BadRequest($"Invalid parameter: {nameof(dto)}");

            var result = await _countryAppService.Create(dto);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]CountryDto dto)
        {
            if (dto == null)
                return BadRequest($"Invalid parameter: {nameof(dto)}");

            var result = await _countryAppService.Update(dto);

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
