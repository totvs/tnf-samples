using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Application.Services;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface ISpecialtyAppService : IApplicationService
    {
        Task<IListDto<SpecialtyDto, int>> GetAllSpecialties(GetAllSpecialtiesDto request);
        Task<SpecialtyDto> GetSpecialty(IRequestDto id);
        Task DeleteSpecialty(int id);
        Task<SpecialtyDto> CreateSpecialty(SpecialtyDto specialty);
        Task<SpecialtyDto> UpdateSpecialty(int id, SpecialtyDto specialty);
    }
}
