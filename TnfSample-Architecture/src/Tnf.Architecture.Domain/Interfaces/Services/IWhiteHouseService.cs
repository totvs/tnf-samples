using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Domain.Services;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IWhiteHouseService : IDomainService
    {
        Task<President> GetPresidentById(RequestDto<string> id);
        Task<string> InsertPresidentAsync(PresidentBuilder builder);
        Task UpdatePresidentAsync(PresidentBuilder builder);
        Task DeletePresidentAsync(string id);
    }
}