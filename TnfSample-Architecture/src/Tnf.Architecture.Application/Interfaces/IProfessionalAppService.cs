using Tnf.Application.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IProfessionalAppService : IApplicationService
    {
        PagingResponseDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto dto);
        ProfessionalDto GetProfessional(ProfessionalKeysDto keys);
        DtoResponseBase<ProfessionalDto> CreateProfessional(ProfessionalCreateDto dto);
        DtoResponseBase<ProfessionalDto> UpdateProfessional(ProfessionalDto dto);
        void DeleteProfessional(ProfessionalKeysDto keys);

        PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(int id);
        void DeleteSpecialty(int id);
        DtoResponseBase<SpecialtyDto> CreateSpecialty(SpecialtyDto dto);
        DtoResponseBase<SpecialtyDto> UpdateSpecialty(SpecialtyDto dto);
    }
}
