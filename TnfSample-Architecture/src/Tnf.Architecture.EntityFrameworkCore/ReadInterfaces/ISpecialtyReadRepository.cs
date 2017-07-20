using Tnf.App.Dto.Response;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.EntityFrameworkCore.ReadInterfaces
{
    public interface ISpecialtyReadRepository : IRepository
    {
        ListDto<SpecialtyDto, int> GetAllSpecialties(GetAllSpecialtiesDto request);
    }
}
