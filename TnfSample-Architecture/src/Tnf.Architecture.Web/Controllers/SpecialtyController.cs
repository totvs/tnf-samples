using Microsoft.AspNetCore.Mvc;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Request;

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
        public IActionResult Get([FromQuery]GetAllSpecialtiesDto requestDto)
        {
            var response = _specialtyAppService.GetAllSpecialties(requestDto);
            
            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery]RequestDto<int> requestDto)
        {
            var response = _specialtyAppService.GetSpecialty(requestDto.AddKey(id));

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpPost]
        public IActionResult Post([FromBody]SpecialtyDto specialty)
        {
            var response = _specialtyAppService.CreateSpecialty(specialty);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SpecialtyDto specialty)
        {
            var response = _specialtyAppService.UpdateSpecialty(id, specialty);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _specialtyAppService.DeleteSpecialty(id);

            return StatusCode(response.GetHttpStatus(), response);
        }
    }
}
