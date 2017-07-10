using System.Linq;
using Tnf.App.Crud;
using Tnf.App.Domain.Repositories;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Extensions;

namespace Tnf.Architecture.Application.Services
{
    public class CountryAppService : AsyncCrudApplicationService<CountryPoco, CountryDto, int, GetAllCountriesDto>, ICountryAppService
    {
        public CountryAppService(IAppRepository<CountryPoco, int> repository)
            : base(repository)
        {
        }

        protected override IQueryable<CountryPoco> CreateFilteredQuery(GetAllCountriesDto input)
        {
            return base.CreateFilteredQuery(input)
                .Where(m => input.Name.IsNullOrWhiteSpace() || m.Name.Contains(input.Name));
        }
    }
}
