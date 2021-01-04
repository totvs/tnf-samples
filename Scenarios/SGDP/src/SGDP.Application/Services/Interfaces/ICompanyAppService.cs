using System;
using System.Threading.Tasks;
using SGDP.Dto;
using SGDP.Dto.Company;
using Tnf.Dto;

namespace SGDP.Application.Services.Interfaces
{
    public interface ICompanyAppService
    {
        Task<IListDto<CompanyDto>> GetAllAsync(CompanyRequestAllDto request);
        Task<CompanyDto> GetAsync(DefaultRequestDto request);
        Task<CompanyDto> CreateAsync(CompanyDto companyDto);
        Task<CompanyDto> UpdateAsync(Guid id, CompanyDto companyDto);
        Task DeleteAsync(Guid id);
    }
}
