using BasicCrud.Application.AppServices.Interfaces;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto.Customer;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace BasicCrud.Application.AppServices
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
            ValidateDto<CustomerDto, Guid>(dto);

            if (Notification.HasNotification())
                return CustomerDto.NullInstance;                       

            var builder = Customer.Create(Notification)
                .WithId(dto.Id)
                .WithName(dto.Name);

            dto.Id = await service.InsertAndGetIdAsync(builder);

            return dto;
        }

        public async Task Delete(Guid id)
        {
            ValidateId(id);

            if (id == Guid.Empty)
                RaiseNotification(Error.ApplicationServiceOnInvalidIdError);

            if (Notification.HasNotification())
                return;

            await service.DeleteAsync(id);
        }

        public async Task<CustomerDto> Get(IRequestDto<Guid> id)
        {
            ValidateId(id);

            if (Notification.HasNotification())
                return CustomerDto.NullInstance;

            if (id.GetId() == Guid.Empty)
                RaiseNotification(Error.ApplicationServiceOnInvalidIdError);

            if (Notification.HasNotification())
                return CustomerDto.NullInstance;

            var entity = await service.GetAsync(id);

            return entity.MapTo<CustomerDto>();
        }

        public async Task<IListDto<CustomerDto, Guid>> GetAll(CustomerRequestAllDto request)
            => await service.GetAllAsync<CustomerDto>(request);

        public async Task<CustomerDto> Update(Guid id, CustomerDto dto)
        {
            ValidateDtoAndId(dto, id);

            if (id == Guid.Empty)
                RaiseNotification(Error.ApplicationServiceOnInvalidIdError);

            if (Notification.HasNotification())
                return CustomerDto.NullInstance;

            var builder = Customer.Create(Notification)
                .WithId(dto.Id)
                .WithName(dto.Name);

            await service.UpdateAsync(builder);

            dto.Id = id;
            return dto;
        }
    }
}
