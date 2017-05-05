using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Paging;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IProfessionalRepository : IRepository
    {
        PagingResponseDto<ProfessionalDto> All(GetAllProfessionalsDto request);
        ProfessionalDto Get(ProfessionalKeysDto keys);
        ProfessionalDto Insert(ProfessionalCreateDto entity);
        ProfessionalDto Update(ProfessionalDto entity);
        void Delete(ProfessionalKeysDto keys);
    }
}
