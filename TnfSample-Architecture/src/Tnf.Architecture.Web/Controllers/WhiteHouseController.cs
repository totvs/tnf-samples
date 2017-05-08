using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Web.Controllers
{
    [Route("api/white-house")]
    public class WhiteHouseController : ArchitectureControllerBase
    {
        private readonly IWhiteHouseAppService _whiteHouseAppService;

        public WhiteHouseController(IWhiteHouseAppService whiteHouseAppService)
        {
            _whiteHouseAppService = whiteHouseAppService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery]int offset, [FromQuery] int pageSize, [FromQuery]string name, [FromQuery]string zipCode)
        {
            if (pageSize <= 0)
                return BadRequest($"Invalid parameter: {nameof(pageSize)}");
            
            var response = await _whiteHouseAppService.GetAllPresidents(new GellAllPresidentsDto(offset, pageSize, name, zipCode));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var president = await _whiteHouseAppService.GetPresidentById(id);
            if (president == null)
                return BadRequest("President not found");

            return Ok(president);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<PresidentDto> presidents, [FromQuery]bool? sync)
        {
            if (presidents == null)
                return BadRequest($"Invalid parameter: {nameof(presidents)}");

            if (presidents.Count <= 0)
                return BadRequest($"Invalid parameter: {nameof(presidents)}");

            if (sync == true)
            {
                var responseSync = await _whiteHouseAppService.InsertPresidentAsync(presidents);
                return Ok(responseSync);
            }

            var response = await _whiteHouseAppService.InsertPresidentAsync(presidents);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]PresidentDto president)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Invalid parameter: {nameof(president)}");

            await _whiteHouseAppService.UpdatePresidentAsync(president);

            return Ok(president);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest($"Invalid parameter: {nameof(id)}");

            await _whiteHouseAppService.DeletePresidentAsync(id);

            return Ok();
        }
    }
}
