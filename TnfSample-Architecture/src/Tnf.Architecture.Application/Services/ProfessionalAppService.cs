using Tnf.Application.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.Paging;
using Tnf.Dto;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : ApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalService _service;

        public ProfessionalAppService(IProfessionalService service)
        {
            _service = service;
        }

        public PagingResponseDto<ProfessionalDto> All(GetAllProfessionalsDto dto) => _service.All(dto);

        public DtoResponseBase<ProfessionalDto> Create(ProfessionalCreateDto dto) => _service.Create(dto);

        public void Delete(ProfessionalKeysDto keys) => _service.Delete(keys);

        public ProfessionalDto Get(ProfessionalKeysDto keys) => _service.Get(keys);

        public DtoResponseBase<ProfessionalDto> Update(ProfessionalDto dto) => _service.Update(dto);
    }
}
