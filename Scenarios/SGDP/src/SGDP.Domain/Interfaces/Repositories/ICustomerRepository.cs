using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SGDP.Domain.Entities;
using SGDP.Dto;
using SGDP.Dto.Customer;
using Tnf.Dto;
using Tnf.Repositories;

namespace SGDP.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository: IRepository
    {
        Task<Customer> GetCustomerAsync(DefaultRequestDto key);

        Task<Customer> GetCustomerAsync(Expression<Func<Customer, bool>> predicate);

        Task<object> GetCustomerNationalIdentificationAsync(Guid id);

        Task<IListDto<CustomerDto>> GetAllCustomersAsync(CustomerRequestAllDto key);

        Task<Customer> InsertCustomerAsync(Customer customer);

        Task<Customer> UpdateCustomerAsync(Customer customer, params Expression<Func<Customer, object>>[] changedProperties);

        Task DeleteCustomerAsync(Guid id);
    }
}
