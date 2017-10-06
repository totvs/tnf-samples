using System.Threading.Tasks;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Carol.ReadInterfaces
{
    public interface IWhiteHouseReadRepository : IRepository
    {
        Task<IListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request);
    }
}
