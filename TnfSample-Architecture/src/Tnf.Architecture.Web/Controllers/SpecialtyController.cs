using Microsoft.AspNetCore.Mvc;
using Tnf.App.Dto.Request;
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
        public IActionResult Get([FromQuery]GetAllSpecialtiesDto requestDto)
        {
            var response = _specialtyAppService.GetAllSpecialties(requestDto);

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.GetAllSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.GetAllSpecialty)
                        .WithDto(response)
                        .Build();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery]RequestDto<int> requestDto)
        {
            var response = _specialtyAppService.GetSpecialty(requestDto.WithId(id));

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.GetSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.GetSpecialty)
                        .WithDto(response)
                        .Build();
        }

        [HttpPost]
        public IActionResult Post([FromBody]SpecialtyDto specialty)
        {
            var response = _specialtyAppService.CreateSpecialty(specialty);

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.PostSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.PostSpecialty)
                        .WithDto(response)
                        .Build();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SpecialtyDto specialty)
        {
            var response = _specialtyAppService.UpdateSpecialty(id, specialty);

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.PutSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.PutSpecialty)
                        .WithDto(response)
                        .Build();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _specialtyAppService.DeleteSpecialty(id);

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.DeleteSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.DeleteSpecialty)
                        .Build();
        }
    }
}
