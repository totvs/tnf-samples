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
        public async Task<IActionResult> Get([FromQuery]GetAllPresidentsDto requestDto)
        {
            var response = await _whiteHouseAppService.GetAllPresidents(requestDto);
            
            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, [FromQuery]RequestDto<string> requestDto)
        {
            var response = await _whiteHouseAppService.GetPresidentById(requestDto.WithId(id));

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PresidentDto president, [FromQuery]bool? sync)
        {
            var response = await _whiteHouseAppService.InsertPresidentAsync(president);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]PresidentDto president)
        {
            var response = await _whiteHouseAppService.UpdatePresidentAsync(id, president);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _whiteHouseAppService.DeletePresidentAsync(id);

            return StatusCode(response.GetHttpStatus(), response);
        }
    }
}
