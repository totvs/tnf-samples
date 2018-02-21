using Case5.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tnf.Repositories;
using Tnf.EntityFrameworkCore.Repositories;
using Case5.Infra.Context;
using Tnf.EntityFrameworkCore;

namespace Case5.Infra
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {

    }

    public class CustomerRepository : EfCoreRepositoryBase<CustomerDbContext, Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(IDbContextProvider<CustomerDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public override void Delete(Guid id)
        {
            base.Delete(id);
        }
    }
}
