using Tnf.Application.Services;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Application.Services
{
    public interface ICountryAppService : IAsyncCrudAppService<CountryDto>
    {
    }
}