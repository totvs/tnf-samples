using System.Threading.Tasks;
using Tnf.App.Crud;
using Tnf.App.Domain.Repositories;
using Tnf.App.Dto.Response;
using Tnf.App.Specifications;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common;
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
            AddCreateSpecification(new ExpressionSpecification<CountryDto>(
                AppConsts.LocalizationSourceName,
                CountryDto.Error.CountryNameMustHaveValue,
                c => !c.Name.IsNullOrEmpty()));

            AddUpdateSpecification(new ExpressionSpecification<CountryDto>(
                AppConsts.LocalizationSourceName,
                CountryDto.Error.CountryNameMustHaveValue,
                c => !c.Name.IsNullOrEmpty()));
        }

        protected override async Task<ListDto<CountryDto, int>> CreateFilteredQueryAsync(GetAllCountriesDto input)
            => await CreateFilteredQueryAsync(m => input.Name.IsNullOrWhiteSpace() || m.Name.Contains(input.Name), input);
    }
}
