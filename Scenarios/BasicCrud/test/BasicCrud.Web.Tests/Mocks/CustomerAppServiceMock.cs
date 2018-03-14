using BasicCrud.Application.AppServices.Interfaces;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace BasicCrud.Web.Tests.Mocks
{
    public class CustomerAppServiceMock : ICustomerAppService
    {
        public static Guid customerGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<CustomerDto> list = new List<CustomerDto>() {
            new CustomerDto() { Id = customerGuid, Name="Customer A" },
            new CustomerDto() { Id = Guid.NewGuid(), Name="Customer B" },
            new CustomerDto() { Id = Guid.NewGuid(), Name="Customer C" }
        };

        public Task<CustomerDto> Create(CustomerDto dto)
        {
            if (dto == null)
                return  Task.FromResult<CustomerDto>(null);

            dto.Id = Guid.NewGuid();
            list.Add(dto);

            return dto.AsTask();
        }

        public Task Delete(Guid id)
        {
            list.RemoveAll(c => c.Id == id);

            return 0.AsTask();
        }

        public Task<CustomerDto> Get(IRequestDto<Guid> id)
        {
            var dto = list.FirstOrDefault(c => c.Id == id.GetId());

            return dto.AsTask();
        }

        public Task<IListDto<CustomerDto, Guid>> GetAll(CustomerRequestAllDto request)
        {
            IListDto<CustomerDto, Guid> result = new ListDto<CustomerDto, Guid> { HasNext = false, Items = list };

            return result.AsTask();
        }

        public Task<CustomerDto> Update(Guid id, CustomerDto dto)
        {
            if (dto == null)
                return Task.FromResult<CustomerDto>(null);

            var oldDto = list.FirstOrDefault(c => c.Id == id);

            if (oldDto == null)
                return Task.FromResult<CustomerDto>(null);

            oldDto.Name = dto.Name;
            return oldDto.AsTask();
        }
    }
}
