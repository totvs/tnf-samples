using Tnf.App.Crud;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface ICountryAppService : IAsyncCrudApplicationService<CountryDto, int, GetAllCountriesDto>
    {
    }
}