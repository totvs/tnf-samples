using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Dto.Request;

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
        public async Task<IActionResult> Get(GetAllPresidentsDto requestDto)
        {
            if (requestDto == null)
                return BadRequest($"Invalid parameter: {nameof(requestDto)}");

            if (requestDto.PageSize <= 0)
                return BadRequest($"Invalid parameter: {nameof(requestDto.PageSize)}");

            var response = await _whiteHouseAppService.GetAllPresidents(requestDto);
            
            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, RequestDto<string> requestDto)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var president = await _whiteHouseAppService.GetPresidentById(requestDto.AddKey(id));
            if (president == null)
                return NotFound(L("CouldNotFindPresident"));

            return Ok(president);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PresidentDto president, [FromQuery]bool? sync)
        {
            if (president == null)
                return BadRequest($"Invalid parameter: {nameof(president)}");

            var response = await _whiteHouseAppService.InsertPresidentAsync(president);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]PresidentDto president)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Invalid parameter: {nameof(id)}");

            if (president == null)
                return BadRequest($"Invalid parameter: {nameof(president)}");

            president.Id = id;
            var response = await _whiteHouseAppService.UpdatePresidentAsync(president);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var response = await _whiteHouseAppService.DeletePresidentAsync(id);

            return StatusCode(response.GetHttpStatus(), response);
        }
    }
}
