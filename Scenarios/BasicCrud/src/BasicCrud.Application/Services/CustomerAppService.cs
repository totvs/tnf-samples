using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Domain.Entities;
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
        private readonly IDomainService<Customer, Guid> service;

        public CustomerAppService(IDomainService<Customer, Guid> service, INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            this.service = service;
        }

        public async Task<CustomerDto> Create(CustomerDto dto)
        {
            if (!ValidateDto<CustomerDto, Guid>(dto))
                return CustomerDto.NullInstance;                       

            var builder = Customer.Create(Notification)
                .WithName(dto.Name);

            dto.Id = await service.InsertAndGetIdAsync(builder);

            return dto;
        }

        public async Task Delete(Guid id)
        {
            if (!ValidateId(id))
                return;

            await service.DeleteAsync(id);
        }

        public async Task<CustomerDto> Get(IRequestDto<Guid> id)
        {
            if (!ValidateRequestDto<IRequestDto<Guid>, Guid>(id))
                return CustomerDto.NullInstance;

            var entity = await service.GetAsync(id);

            return entity.MapTo<CustomerDto>();
        }

        public async Task<IListDto<CustomerDto, Guid>> GetAll(CustomerRequestAllDto request)
            => await service.GetAllAsync<CustomerDto>(request, c => request.Name.IsNullOrEmpty() || c.Name.Contains(request.Name));

        public async Task<CustomerDto> Update(Guid id, CustomerDto dto)
        {
            if (!ValidateDtoAndId(dto, id))
                return CustomerDto.NullInstance;

            var builder = Customer.Create(Notification)
                .WithId(id)
                .WithName(dto.Name);

            await service.UpdateAsync(builder);

            dto.Id = id;
            return dto;
        }
    }
}
