using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Domain.Registration;
using Tnf.Domain.Services;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface ISpecialtyService : IDomainService
    {
        Task<Specialty> GetSpecialty(IRequestDto requestDto);
        Task DeleteSpecialty(int id);
        Task<int> CreateSpecialty(SpecialtyBuilder builder);
        Task UpdateSpecialty(SpecialtyBuilder builder);
    }
}
