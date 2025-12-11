using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;
using Tnf.Dto;
using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories;

public interface ICustomerRepository : IRepository
{
    Task<Customer> GetAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<IListDto<CustomerDto>> GetAllAsync(RequestAllDto requestAllDto, CancellationToken cancellationToken = default);
    Task<Customer> InsertAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid customerId, CancellationToken cancellationToken = default);
}
