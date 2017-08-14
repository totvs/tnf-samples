using System.Threading.Tasks;
using Tnf.App.Crud;
using Tnf.App.Domain.Repositories;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Application.Services.Specifications;
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
            AddCreateSpecification(new CountryMustHaveHaveValueSpecification());
            AddUpdateSpecification(new CountryMustHaveHaveValueSpecification());
        }

        protected override async Task<ListDto<CountryDto, int>> CreateFilteredQueryAsync(GetAllCountriesDto input)
            => await CreateFilteredQueryAsync(input, m => input.Name.IsNullOrWhiteSpace() || m.Name.Contains(input.Name));
    }
}
