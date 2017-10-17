using System.Threading.Tasks;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.EntityFrameworkCore.ReadInterfaces
{
    public interface ISpecialtyReadRepository : IRepository
    {
        Task<IListDto<SpecialtyDto, int>> GetAllSpecialties(GetAllSpecialtiesDto request);
    }
}
