using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SGDP.Domain.Entities;
using SGDP.Domain.Interfaces.Repositories;
using SGDP.Dto;
using SGDP.Dto.Company;
using SGDP.Infra.Context;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.Sgdp;

namespace SGDP.Infra.Repositories
{
    [SgdpCode("Validar o LGPD do TNF, sobre a identificação, auditoria e anonimização de dados pessoais")]
    public class CompanyRepository : EfCoreRepositoryBase<OrderDbContext, Company>, ICompanyRepository
    {
        public CompanyRepository(IDbContextProvider<OrderDbContext> dbContextProvider, ISgdpLogger sgdpLogger)
            : base(dbContextProvider)
        { }

        public async Task<IListDto<CompanyDto>> GetAllCompaniesAsync(CompanyRequestAllDto key)
        {
            return await GetAllAsync<CompanyDto>(key,
                c => (key.Cnpj.IsNullOrEmpty() || c.Cnpj.Equals(key.Cnpj))
                    && (key.Email.IsNullOrEmpty() || c.Email.Equals(key.Email)));
        }

        public async Task<Company> GetCompanyAsync(DefaultRequestDto requestDto)
            => await GetAsync(requestDto);

        public async Task<Company> InsertCompanyAsync(Company company)
            => await InsertAndSaveChangesAsync(company);

        public async Task<Company> UpdateCompanyAsync(Company company, params Expression<Func<Company, object>>[] changedProperties)
            => await UpdateAsync(company, changedProperties: changedProperties);

        public async Task DeleteCompanyAsync(Guid id)
            => await DeleteAsync(c => c.Id == id);
    }
}
