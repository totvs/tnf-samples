using BasicCrud.Dto.Customer;
using System;
using Tnf.Application.Services;

namespace BasicCrud.Application.Services.Interfaces
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface ICustomerAppService : IAsyncApplicationService<CustomerDto, CustomerRequestAllDto, Guid>
    {
    }
}
