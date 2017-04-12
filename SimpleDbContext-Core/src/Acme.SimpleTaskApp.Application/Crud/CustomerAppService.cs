using Acme.SimpleTaskApp.Crud.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Domain.Repositories;

namespace Acme.SimpleTaskApp.Crud
{
    public class CustomerAppService : CrudAppService<Customer, CustomerDto>
    {
        public CustomerAppService(IRepository<Customer, int> repository) : base(repository)
        {
        }
    }
}
