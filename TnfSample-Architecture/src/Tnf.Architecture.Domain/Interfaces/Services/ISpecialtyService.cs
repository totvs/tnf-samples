using Tnf.App.Dto.Request;
using Tnf.Architecture.Domain.Registration;
using Tnf.Domain.Services;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface ISpecialtyService : IDomainService
    {
        Specialty GetSpecialty(IRequestDto requestDto);
        void DeleteSpecialty(int id);
        int CreateSpecialty(SpecialtyBuilder builder);
        void UpdateSpecialty(SpecialtyBuilder builder);
    }
}
