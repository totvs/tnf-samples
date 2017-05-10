using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IProfessionalService : IDomainService
    {
        PagingResponseDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(ProfessionalKeysDto keys);
        DtoResponseBase<ProfessionalDto> CreateProfessional(ProfessionalCreateDto entity);
        DtoResponseBase<ProfessionalDto> UpdateProfessional(ProfessionalDto dto);
        DtoResponseBase DeleteProfessional(ProfessionalKeysDto keys);

        PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(int id);
        DtoResponseBase DeleteSpecialty(int id);
        DtoResponseBase<SpecialtyDto> CreateSpecialty(SpecialtyDto dto);
        DtoResponseBase<SpecialtyDto> UpdateSpecialty(SpecialtyDto dto);
    }
}
