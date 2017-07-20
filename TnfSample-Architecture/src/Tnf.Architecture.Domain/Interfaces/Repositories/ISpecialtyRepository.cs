using Tnf.App.Dto.Request;
using Tnf.Architecture.Domain.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface ISpecialtyRepository : IRepository
    {
        Specialty GetSpecialty(RequestDto requestDto);
        int CreateSpecialty(Specialty entity);
        void UpdateSpecialty(Specialty entity);
        void DeleteSpecialty(int id);
        bool ExistsSpecialty(int id);
    }
}
