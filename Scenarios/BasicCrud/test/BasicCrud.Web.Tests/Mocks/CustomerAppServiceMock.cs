using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Dto;
using BasicCrud.Dto.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dto;

namespace BasicCrud.Web.Tests.Mocks
{
    public class CustomerAppServiceMock : ICustomerAppService
    {
        public static Guid customerGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<CustomerDto> list = new List<CustomerDto>() {
            new CustomerDto() { Id = customerGuid, Name = "Customer A" },
            new CustomerDto() { Id = Guid.NewGuid(), Name = "Customer B" },
            new CustomerDto() { Id = Guid.NewGuid(), Name = "Customer C" }
        };

        public Task<CustomerDto> CreateAsync(CustomerDto dto)
        {
            if (dto == null)
                return Task.FromResult<CustomerDto>(null);

            dto.Id = Guid.NewGuid();
            list.Add(dto);

            return dto.AsTask();
        }

        public Task DeleteAsync(Guid id)
        {
            list.RemoveAll(c => c.Id == id);

            return Task.CompletedTask;
        }

        public Task<CustomerDto> GetAsync(DefaultRequestDto id)
        {
            var dto = list.FirstOrDefault(c => c.Id == id.Id);

            return dto.AsTask();
        }

        public Task<IListDto<CustomerDto>> GetAllAsync(CustomerRequestAllDto request)
        {
            IListDto<CustomerDto> result = new ListDto<CustomerDto> { HasNext = false, Items = list };

            return result.AsTask();
        }

        public Task<CustomerDto> UpdateAsync(Guid id, CustomerDto dto)
        {
            if (dto == null)
                return Task.FromResult<CustomerDto>(null);

            list.RemoveAll(c => c.Id == id);
            dto.Id = id;
            list.Add(dto);

            return dto.AsTask();
        }
    }
}
