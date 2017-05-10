using Microsoft.AspNetCore.Mvc;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.Specialty)]
    public class SpecialtyController : ArchitectureControllerBase
    {
        private readonly IProfessionalAppService _professionalAppService;

        public SpecialtyController(IProfessionalAppService professionalAppService)
        {
            _professionalAppService = professionalAppService;
        }

        [HttpGet("")]
        public IActionResult Get(GetAllSpecialtiesDto requestDto)
        {
            if (requestDto == null)
                return BadRequest($"Invalid parameter: {nameof(requestDto)}");

            if (requestDto.PageSize <= 0)
                return BadRequest($"Invalid parameter: {nameof(requestDto.PageSize)}");

            var response = _professionalAppService.GetAllSpecialties(requestDto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var result = _professionalAppService.GetSpecialty(id);
            if (result == null)
                return NotFound("Specialty not found");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]SpecialtyDto dto)
        {
            if (dto == null)
                return BadRequest($"Invalid parameter: {nameof(dto)}");

            var result = _professionalAppService.CreateSpecialty(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SpecialtyDto dto)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            if (dto == null)
                return BadRequest($"Invalid parameter: {nameof(dto)}");

            var result = _professionalAppService.UpdateSpecialty(dto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            _professionalAppService.DeleteSpecialty(id);
            return Ok();
        }
    }
}
