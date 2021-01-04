using System;
using System.Threading.Tasks;
using SGDP.Application.Services.Interfaces;
using SGDP.Domain.Entities;
using SGDP.Domain.Interfaces.Repositories;
using SGDP.Dto;
using SGDP.Dto.Customer;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace SGDP.Application.Services
{
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {
        private readonly ICustomerRepository _repository;

        public CustomerAppService(
            ICustomerRepository repository,
            INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            _repository = repository;
        }

        public async Task<IListDto<CustomerDto>> GetAllAsync(CustomerRequestAllDto request)
            => await _repository.GetAllCustomersAsync(request);

        public async Task<CustomerDto> GetAsync(DefaultRequestDto request)
        {
            if (!ValidateRequestDto(request) || !ValidateId<Guid>(request.Id))
                return null;

            var entity = await _repository.GetCustomerAsync(request);

            return entity.MapTo<CustomerDto>();
        }

        public async Task<object> GetIdentificationsAsync(Guid id)
            => await _repository.GetCustomerNationalIdentificationAsync(id);

        public async Task<CustomerDto> CreateAsync(CustomerDto customerDto)
        {
            if (!ValidateDto<CustomerDto>(customerDto))
                return null;

            var customer = new Customer
            {
                Cpf = customerDto.Cpf,
                Email = customerDto.Email,
                Rg = customerDto.Rg
            };

            var result = await _repository.InsertCustomerAsync(customer);

            return result.MapTo<CustomerDto>();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!ValidateId(id))
                return;

            await _repository.DeleteCustomerAsync(id);
        }

        public async Task<CustomerDto> UpdateAsync(Guid id, CustomerDto customerDto)
        {
            if (!ValidateDtoAndId(customerDto, id))
                return null;

            var customer = new Customer
            {
                Id = id,
                Cpf = customerDto.Cpf,
                Email = customerDto.Email,
                Rg = customerDto.Rg
            };

            var updatedCustomer = await _repository.UpdateCustomerAsync(customer);

            return updatedCustomer.MapTo<CustomerDto>();
        }
    }
}
