using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.WhiteHouse)]
    public class WhiteHouseController : ArchitectureControllerBase
    {
        private readonly IWhiteHouseAppService _whiteHouseAppService;

        public WhiteHouseController(IWhiteHouseAppService whiteHouseAppService)
        {
            _whiteHouseAppService = whiteHouseAppService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(GellAllPresidentsDto requestDto)
        {
            if (requestDto == null)
                return BadRequest($"Invalid parameter: {nameof(requestDto)}");

            if (requestDto.PageSize <= 0)
                return BadRequest($"Invalid parameter: {nameof(requestDto.PageSize)}");

            var response = await _whiteHouseAppService.GetAllPresidents(requestDto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var president = await _whiteHouseAppService.GetPresidentById(id);
            if (president == null)
                return NotFound(L("CouldNotFindPresident"));

            return Ok(president);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<PresidentDto> presidents, [FromQuery]bool? sync)
        {
            if (presidents == null)
                return BadRequest($"Invalid parameter: {nameof(presidents)}");

            if (presidents.Count <= 0)
                return BadRequest($"Empty parameter: {nameof(presidents)}");

            var response = await _whiteHouseAppService.InsertPresidentAsync(presidents);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]PresidentDto president)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Invalid parameter: {nameof(id)}");

            if (president == null)
                return BadRequest($"Invalid parameter: {nameof(president)}");

            president.Id = id;
            var result = await _whiteHouseAppService.UpdatePresidentAsync(president);
            if (result.Data == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var result = await _whiteHouseAppService.DeletePresidentAsync(id);
            if(!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
