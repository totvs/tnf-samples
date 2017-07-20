using Microsoft.AspNetCore.Mvc;
using System;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Dto.Registration;

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

            return CreateResponse<ProfessionalDto, ComposeKey<Guid, decimal>>()
                        .FromErrorEnum(ProfessionalDto.Error.GetAllProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.GetAllProfessional)
                        .WithDto(response)
                        .Build();
        }

        [HttpGet("{professionalId}/{code}")]
        public IActionResult Get(decimal professionalId, Guid code, [FromQuery]RequestDto<ComposeKey<Guid, decimal>> requestDto)
        {
            var response = _professionalAppService.GetProfessional(requestDto.WithId(new ComposeKey<Guid, decimal>(code, professionalId)));

            return CreateResponse<ProfessionalDto, ComposeKey<Guid, decimal>>()
                        .FromErrorEnum(ProfessionalDto.Error.GetProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.GetProfessional)
                        .WithDto(response)
                        .Build();
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProfessionalDto professional)
        {
            var response = _professionalAppService.CreateProfessional(professional);

            return CreateResponse<ProfessionalDto, ComposeKey<Guid, decimal>>()
                        .FromErrorEnum(ProfessionalDto.Error.PostProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.PostProfessional)
                        .WithDto(response)
                        .Build();
        }

        [HttpPut("{professionalId}/{code}")]
        public IActionResult Put(decimal professionalId, Guid code, [FromBody]ProfessionalDto professional)
        {
            var response = _professionalAppService.UpdateProfessional(new ComposeKey<Guid, decimal>(code, professionalId), professional);

            return CreateResponse<ProfessionalDto, ComposeKey<Guid, decimal>>()
                        .FromErrorEnum(ProfessionalDto.Error.PutProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.PutProfessional)
                        .WithDto(response)
                        .Build();
        }

        [HttpDelete("{professionalId}/{code}")]
        public IActionResult Delete(decimal professionalId, Guid code)
        {
            _professionalAppService.DeleteProfessional(new ComposeKey<Guid, decimal>(code, professionalId));

            return CreateResponse<ProfessionalDto, ComposeKey<Guid, decimal>>()
                        .FromErrorEnum(ProfessionalDto.Error.DeleteProfessional)
                        .WithMessage(AppConsts.LocalizationSourceName, ProfessionalDto.Error.DeleteProfessional)
                        .Build();
        }
    }
}
