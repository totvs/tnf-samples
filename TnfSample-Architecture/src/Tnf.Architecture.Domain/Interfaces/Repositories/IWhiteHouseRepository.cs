using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IWhiteHouseRepository : IRepository
    {
        Task<President> GetPresidentById(RequestDto<string> requestDto);
        Task<string> InsertPresidentsAsync(President presidents);
        Task<President> UpdatePresidentsAsync(President president);
        Task<bool> DeletePresidentsAsync(string id);
    }
}
