using Microsoft.EntityFrameworkCore;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore.Repositories;

public class CustomerRepository : EfCoreRepositoryBase<CarShopDbContext, Customer>, ICustomerRepository
{
    public CustomerRepository(IDbContextProvider<CarShopDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task DeleteAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var customer = await GetAsync(customerId, cancellationToken);
        if (customer is null)
            return;

        await base.DeleteAsync(customer, cancellationToken);
    }

    public async Task<IListDto<CustomerDto>> GetAllAsync(RequestAllDto requestAllDto, CancellationToken cancellationToken = default)
    {
        var basequery = GetAll().AsNoTracking();

        return await basequery.Select(x => new CustomerDto
        {
            Id = x.Id,
            Address = x.Address,
            DateOfBirth = x.DateOfBirth,
            Email = x.Email,
            FullName = x.FullName,
            Phone = x.Phone            
        }).ToListDtoAsync(requestAllDto, cancellationToken);
    }

    public async Task<Customer> GetAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await Table.FirstOrDefaultAsync(x => x.Id == customerId, cancellationToken);
    }

    public async Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        return await base.UpdateAsync(customer, cancellationToken);
    }
}
