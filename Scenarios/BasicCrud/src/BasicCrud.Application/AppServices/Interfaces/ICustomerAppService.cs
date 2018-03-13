using BasicCrud.Dto.Customer;
using System;
using Tnf.Application.Services;

namespace BasicCrud.Application.AppServices.Interfaces
{
    public interface ICustomerAppService : IAsyncApplicationService<CustomerDto, CustomerRequestAllDto, Guid>
    {
    }
}
