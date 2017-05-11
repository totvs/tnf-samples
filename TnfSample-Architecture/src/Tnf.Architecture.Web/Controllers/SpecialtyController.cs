using Microsoft.AspNetCore.Mvc;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.Specialty)]
    public class SpecialtyController : ArchitectureControllerBase
    {
        private readonly ISpecialtyAppService _specialtyAppService;

        public SpecialtyController(ISpecialtyAppService specialtyAppService)
        {
            _specialtyAppService = specialtyAppService;
        }

        [HttpGet("")]
        public IActionResult Get(GetAllSpecialtiesDto requestDto)
        {
            if (requestDto == null)
                return BadRequest($"Invalid parameter: {nameof(requestDto)}");

            if (requestDto.PageSize <= 0)
                return BadRequest($"Invalid parameter: {nameof(requestDto.PageSize)}");

            var response = _specialtyAppService.GetAllSpecialties(requestDto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var specialty = _specialtyAppService.GetSpecialty(id);
            if (specialty == null)
                return NotFound(L("CouldNotFindSpecialty"));

            return Ok(specialty);
        }

        [HttpPost]
        public IActionResult Post([FromBody]SpecialtyDto specialty)
        {
            if (specialty == null)
                return BadRequest($"Invalid parameter: {nameof(specialty)}");

            var result = _specialtyAppService.CreateSpecialty(specialty);
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
            var result = _specialtyAppService.UpdateSpecialty(specialty);
            if (result.Data == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid parameter: {nameof(id)}");

            var result = _specialtyAppService.DeleteSpecialty(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
