using Tnf.Application.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Dto;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Services
{
    public class SpecialtyAppService : ApplicationService, ISpecialtyAppService
    {
        private readonly ISpecialtyService _service;

        public SpecialtyAppService(ISpecialtyService service)
        {
            _service = service;
        }

        public PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request) => _service.GetAllSpecialties(request);

        public SpecialtyDto GetSpecialty(int id) => _service.GetSpecialty(id);

        public ResponseDtoBase DeleteSpecialty(int id) => _service.DeleteSpecialty(id);

        public ResponseDtoBase<SpecialtyDto> CreateSpecialty(SpecialtyDto dto) => _service.CreateSpecialty(dto);

        public ResponseDtoBase<SpecialtyDto> UpdateSpecialty(SpecialtyDto dto) => _service.UpdateSpecialty(dto);
    }
}
