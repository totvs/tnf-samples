using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Response;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : ApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalService _service;

        public ProfessionalAppService(IProfessionalService service)
        {
            _service = service;
        }

        public SuccessResponseListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto dto) => _service.GetAllProfessionals(dto);

        public IResponseDto CreateProfessional(ProfessionalDto dto) => _service.CreateProfessional(dto);

        public IResponseDto DeleteProfessional(ProfessionalKeysDto keys) => _service.DeleteProfessional(keys);

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys) => _service.GetProfessional(keys);

        public IResponseDto UpdateProfessional(ProfessionalDto dto) => _service.UpdateProfessional(dto);
    }
}
