using System.Collections;
using System.Collections.Generic;
using Tnf.Dependency;

namespace Case6.Infra.Services
{
    public interface ICustomerService : ITransientDependency
    {
        IEnumerable<CustomerDto> GetAllCustomers();
    }
}
