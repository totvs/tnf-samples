using BasicCrud.Dto.Customer;
using System;
using Tnf.Application.Services;

namespace BasicCrud.Application.Services.Interfaces
{
    public interface ICustomerAppService : IAsyncApplicationService<CustomerDto, CustomerRequestAllDto, Guid>
    {
    }
}
