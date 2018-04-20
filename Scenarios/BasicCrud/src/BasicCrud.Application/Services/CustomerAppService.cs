using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto;
using BasicCrud.Dto.Customer;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace BasicCrud.Application.Services
{
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {
        private readonly IDomainService<Customer> _service;

        public CustomerAppService(IDomainService<Customer> service, INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            _service = service;
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto dto)
        {
            if (!ValidateDto<CustomerDto>(dto))
                return null;                       

            var builder = Customer.Create(Notification)
                .WithName(dto.Name);

            var customer = await _service.InsertAndSaveChangesAsync(builder);

            return customer.MapTo<CustomerDto>();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!ValidateId(id))
                return;

            await _service.DeleteAsync(w => w.Id == id);
        }

        public async Task<CustomerDto> GetAsync(DefaultRequestDto id)
        {
            if (!ValidateRequestDto(id) || !ValidateId<Guid>(id.Id))
                return null;

            var entity = await _service.GetAsync(id);

            return entity.MapTo<CustomerDto>();
        }

        public async Task<IListDto<CustomerDto>> GetAllAsync(CustomerRequestAllDto request)
            => await _service.GetAllAsync<CustomerDto>(request, c => request.Name.IsNullOrEmpty() || c.Name.Contains(request.Name));

        public async Task<CustomerDto> UpdateAsync(Guid id, CustomerDto dto)
        {
            if (!ValidateDtoAndId(dto, id))
                return null;

            var builder = Customer.Create(Notification)
                .WithId(id)
                .WithName(dto.Name);

            await _service.UpdateAsync(builder);

            dto.Id = id;
            return dto;
        }
    }
}
