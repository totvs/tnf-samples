using System;
using System.Threading.Tasks;
using SGDP.Application.Services.Interfaces;
using SGDP.Domain.Entities;
using SGDP.Domain.Interfaces.Repositories;
using SGDP.Dto;
using SGDP.Dto.Company;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace SGDP.Application.Services
{
    public class CompanyAppService : ApplicationService, ICompanyAppService
    {
        private readonly ICompanyRepository _repository;

        public CompanyAppService(
            ICompanyRepository repository,
            INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            _repository = repository;
        }

        public async Task<IListDto<CompanyDto>> GetAllAsync(CompanyRequestAllDto request)
            => await _repository.GetAllCompaniesAsync(request);

        public async Task<CompanyDto> GetAsync(DefaultRequestDto request)
        {
            if (!ValidateRequestDto(request) || !ValidateId<Guid>(request.Id))
                return null;

            var entity = await _repository.GetCompanyAsync(request);

            return entity.MapTo<CompanyDto>();
        }

        public async Task<CompanyDto> CreateAsync(CompanyDto CompanyDto)
        {
            if (!ValidateDto<CompanyDto>(CompanyDto))
                return null;

            var Company = new Company
            {
                Cnpj = CompanyDto.Cnpj,
                Email = CompanyDto.Email
            };

            var result = await _repository.InsertCompanyAsync(Company);

            return result.MapTo<CompanyDto>();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!ValidateId(id))
                return;

            await _repository.DeleteCompanyAsync(id);
        }

        public async Task<CompanyDto> UpdateAsync(Guid id, CompanyDto CompanyDto)
        {
            if (!ValidateDtoAndId(CompanyDto, id))
                return null;

            var Company = new Company
            {
                Id = id,
                Cnpj = CompanyDto.Cnpj,
                Email = CompanyDto.Email
            };

            var updatedCompany = await _repository.UpdateCompanyAsync(Company);

            return updatedCompany.MapTo<CompanyDto>();
        }
    }
}
