using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Application.Services
{
    public class CountryAppService : AsyncCrudAppService<Country, CountryDto>, ICountryAppService
    {
        public CountryAppService(IRepository<Country> repository)
            : base(repository)
        {
        }
    }
}
