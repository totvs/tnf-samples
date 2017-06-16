using Microsoft.AspNetCore.Mvc;
using System;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.App.Dto.Request;

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

            return CreateResponse<ProfessionalDto>()
                        .FromErrorEnum(ProfessionalDto.Error.GetAllProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.GetAllProfessional)
                        .WithDto(response)
                        .Build();
        }

        [HttpGet("{professionalId}/{code}")]
        public IActionResult Get(decimal professionalId, Guid code, [FromQuery]RequestDto<ProfessionalKeysDto> requestDto)
        {
            var response = _professionalAppService.GetProfessional(requestDto.WithId(new ProfessionalKeysDto(professionalId, code)));

            return CreateResponse<ProfessionalDto>()
                        .FromErrorEnum(ProfessionalDto.Error.GetProfessional)
                        .WithNotFoundStatus()
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.GetProfessional)
                        .WithDto(response)
                        .Build();
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProfessionalDto professional)
        {
            var response = _professionalAppService.CreateProfessional(professional);

            return CreateResponse<ProfessionalDto>()
                        .FromErrorEnum(ProfessionalDto.Error.PostProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.PostProfessional)
                        .WithDto(response)
                        .Build();
        }

        [HttpPut("{professionalId}/{code}")]
        public IActionResult Put(decimal professionalId, Guid code, [FromBody]ProfessionalDto professional)
        {
            var response = _professionalAppService.UpdateProfessional(new ProfessionalKeysDto(professionalId, code), professional);

            return CreateResponse<ProfessionalDto>()
                        .FromErrorEnum(ProfessionalDto.Error.PutProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.PutProfessional)
                        .WithDto(response)
                        .Build();
        }

        [HttpDelete("{professionalId}/{code}")]
        public IActionResult Delete(decimal professionalId, Guid code)
        {
            _professionalAppService.DeleteProfessional(new ProfessionalKeysDto(professionalId, code));

            return CreateResponse<ProfessionalDto>()
                        .FromErrorEnum(ProfessionalDto.Error.DeleteProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.DeleteProfessional)
                        .Build();
        }
    }
}
