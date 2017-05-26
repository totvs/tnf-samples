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
        public IActionResult Get([FromQuery]GetAllProfessionalsDto requestDto)
        {
            var response = _professionalAppService.GetAllProfessionals(requestDto);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpGet("{professionalId}/{code}")]
        public IActionResult Get(decimal professionalId, Guid code, [FromQuery]RequestDto<ProfessionalKeysDto> requestDto)
        {
            var response = _professionalAppService.GetProfessional(requestDto.WithId(new ProfessionalKeysDto(professionalId, code)));

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProfessionalDto professional)
        {
            var response = _professionalAppService.CreateProfessional(professional);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpPut("{professionalId}/{code}")]
        public IActionResult Put(decimal professionalId, Guid code, [FromBody]ProfessionalDto professional)
        {
            var response = _professionalAppService.UpdateProfessional(new ProfessionalKeysDto(professionalId, code), professional);

            return StatusCode(response.GetHttpStatus(), response);
        }

        [HttpDelete("{professionalId}/{code}")]
        public IActionResult Delete(decimal professionalId, Guid code)
        {
            var response = _professionalAppService.DeleteProfessional(new ProfessionalKeysDto(professionalId, code));

            return StatusCode(response.GetHttpStatus(), response);
        }
    }
}
