using System;
using System.Threading.Tasks;
using SGDP.Dto;
using SGDP.Dto.Customer;
using Tnf.Dto;

namespace SGDP.Application.Services.Interfaces
{
    public interface ICustomerAppService
    {
        Task<IListDto<CustomerDto>> GetAllAsync(CustomerRequestAllDto request);
        Task<CustomerDto> GetAsync(DefaultRequestDto request);
        Task<object> GetIdentificationsAsync(Guid id);
        Task<CustomerDto> CreateAsync(CustomerDto customerDto);
        Task<CustomerDto> UpdateAsync(Guid id, CustomerDto customerDto);
        Task DeleteAsync(Guid id);
    }
}
