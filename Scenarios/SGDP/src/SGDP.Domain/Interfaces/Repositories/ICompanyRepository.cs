using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SGDP.Domain.Entities;
using SGDP.Dto;
using SGDP.Dto.Company;
using Tnf.Dto;
using Tnf.Repositories;

namespace SGDP.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository: IRepository
    {
        Task<Company> GetCompanyAsync(DefaultRequestDto key);

        Task<IListDto<CompanyDto>> GetAllCompaniesAsync(CompanyRequestAllDto key);

        Task<Company> InsertCompanyAsync(Company Company);

        Task<Company> UpdateCompanyAsync(Company Company, params Expression<Func<Company, object>>[] changedProperties);

        Task DeleteCompanyAsync(Guid id);
    }
}
