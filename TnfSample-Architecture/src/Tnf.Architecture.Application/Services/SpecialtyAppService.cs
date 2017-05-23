using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Application.Services
{
    public class SpecialtyAppService : ApplicationService, ISpecialtyAppService
    {
        private readonly ISpecialtyService _service;

        public SpecialtyAppService(ISpecialtyService service)
        {
            _service = service;
        }

        public SuccessResponseListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request) => _service.GetAllSpecialties(request);

        public SpecialtyDto GetSpecialty(RequestDto<int> requestDto) => _service.GetSpecialty(requestDto);

        public IResponseDto DeleteSpecialty(int id) => _service.DeleteSpecialty(id);

        public IResponseDto CreateSpecialty(SpecialtyDto dto) => _service.CreateSpecialty(dto);

        public IResponseDto UpdateSpecialty(SpecialtyDto dto) => _service.UpdateSpecialty(dto);
    }
}
