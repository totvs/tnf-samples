using Tnf.Application.Services;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface ICountryAppService : IAsyncCrudAppService<CountryDto>
    {
    }
}