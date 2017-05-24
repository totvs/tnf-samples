using Microsoft.AspNetCore.Mvc;
using System;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Request;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.Professional)]
    public class ProfessionalController : ArchitectureControllerBase
    {
        private readonly IProfessionalAppService _professionalAppService;

        public ProfessionalController(IProfessionalAppService professionalAppService)
        {
            _professionalAppService = professionalAppService;
        }

        [HttpGet("")]
        public IActionResult Get([FromQuery] GetAllProfessionalsDto requestDto)
        {
            if (requestDto == null)
                return BadRequest($"Invalid parameter: {nameof(requestDto)}");

            if (requestDto.PageSize <= 0)
                return BadRequest($"Invalid parameter: {nameof(requestDto.PageSize)}");

            var response = _professionalAppService.GetAllProfessionals(requestDto);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpGet("{professionalId}/{code}")]
        public IActionResult Get(decimal professionalId, Guid code, RequestDto<ProfessionalKeysDto> requestDto)
        {
            if (professionalId <= 0)
                return BadRequest($"Invalid parameter: {nameof(professionalId)}");

            if (code == Guid.Empty)
                return BadRequest($"Invalid parameter: {nameof(code)}");

            var result = _professionalAppService.GetProfessional(requestDto.AddKey(new ProfessionalKeysDto(professionalId, code)));
            if (result == null)
                return NotFound(L("CouldNotFindProfessional"));

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProfessionalDto professional)
        {
            if (professional == null)
                return BadRequest($"Invalid parameter: {nameof(professional)}");

            var response = _professionalAppService.CreateProfessional(professional);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpPut("{professionalId}/{code}")]
        public IActionResult Put(decimal professionalId, Guid code, [FromBody]ProfessionalDto professional)
        {
            if (professionalId <= 0)
                return BadRequest($"Invalid parameter: {nameof(professionalId)}");

            if (code == Guid.Empty)
                return BadRequest($"Invalid parameter: {nameof(code)}");

            if (professional == null)
                return BadRequest($"Invalid parameter: {nameof(professional)}");

            professional.ProfessionalId = professionalId;
            professional.Code = code;

            var response = _professionalAppService.UpdateProfessional(professional);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpDelete("{professionalId}/{code}")]
        public IActionResult Delete(decimal professionalId, Guid code)
        {
            if (professionalId <= 0)
                return BadRequest($"Invalid parameter: {nameof(professionalId)}");

            if (code == Guid.Empty)
                return BadRequest($"Invalid parameter: {nameof(code)}");

            var response = _professionalAppService.DeleteProfessional(new ProfessionalKeysDto(professionalId, code));

            return StatusCode(response.GetHttpStatus(), response);
        }
    }
}
