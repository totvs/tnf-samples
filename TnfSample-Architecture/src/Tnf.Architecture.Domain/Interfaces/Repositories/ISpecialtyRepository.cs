using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Domain.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface ISpecialtyRepository : IRepository
    {
        Task<Specialty> GetSpecialty(IRequestDto requestDto);
        Task<int> CreateSpecialty(Specialty entity);
        Task UpdateSpecialty(Specialty entity);
        Task DeleteSpecialty(int id);
        Task<bool> ExistsSpecialty(int id);
    }
}
