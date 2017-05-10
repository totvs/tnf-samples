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

            var specialty = _professionalAppService.GetSpecialty(id);
            if (specialty == null)
                return NotFound(L("CouldNotFindSpecialty"));

            return Ok(specialty);
        }

        [HttpPost]
        public IActionResult Post([FromBody]SpecialtyDto specialty)
        {
            if (specialty == null)
                return BadRequest($"Invalid parameter: {nameof(specialty)}");

            var result = _professionalAppService.CreateSpecialty(specialty);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SpecialtyDto specialty)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            if (specialty == null)
                return BadRequest($"Invalid parameter: {nameof(specialty)}");

            specialty.Id = id;
            var result = _professionalAppService.UpdateSpecialty(specialty);
            if (result.Data == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var result = _professionalAppService.DeleteSpecialty(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
