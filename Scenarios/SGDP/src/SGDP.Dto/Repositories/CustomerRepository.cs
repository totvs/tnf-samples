using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGDP.Domain.Entities;
using SGDP.Domain.Interfaces.Repositories;
using SGDP.Dto;
using SGDP.Dto.Customer;
using SGDP.Infra.Context;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.Sgdp;

namespace SGDP.Infra.Repositories
{
    [SgdpCode("Validar o LGPD do TNF, sobre a identificação, auditoria e anonimização de dados pessoais")]
    public class CustomerRepository : EfCoreRepositoryBase<OrderDbContext, Customer>, ICustomerRepository
    {
        private readonly ISgdpLogger _sgdpLogger;

        public CustomerRepository(IDbContextProvider<OrderDbContext> dbContextProvider, ISgdpLogger sgdpLogger)
            : base(dbContextProvider)
        {
            _sgdpLogger = sgdpLogger;
        }

        public async Task<IListDto<CustomerDto>> GetAllCustomersAsync(CustomerRequestAllDto key)
        {
            return await GetAllAsync<CustomerDto>(key,
                c => (key.Cpf.IsNullOrEmpty() || c.Cpf.Equals(key.Cpf))
                    && (key.Email.IsNullOrEmpty() || c.Email.Equals(key.Email))
                    && (key.Rg.IsNullOrEmpty() || c.Rg.Equals(key.Rg)));
        }

        public async Task<object> GetCustomerNationalIdentificationAsync(Guid id)
        {
            var customerData = await GetAll(c => c.Id == id)
                .Select(c => new {
                    c.Cpf,
                    c.Rg
                })
                .FirstOrDefaultAsync();

            // Registro manual do audit log Sgdp.
            // Quando dados Sgdp são acessados através de projection o sdk Tnf.Sgdp não gera os audit logs automaticamente
            await _sgdpLogger.LogAsync(DataProcessingActions.Load, customerData, typeof(Customer));

            return customerData;
        }

        public async Task<Customer> GetCustomerAsync(DefaultRequestDto requestDto)
            => await GetAsync(requestDto);

        public async Task<Customer> GetCustomerAsync(Expression<Func<Customer, bool>> predicate)
            => await FirstOrDefaultAsync(predicate);

        public async Task<Customer> InsertCustomerAsync(Customer customer)
            => await InsertAndSaveChangesAsync(customer);

        public async Task<Customer> UpdateCustomerAsync(Customer customer, params Expression<Func<Customer, object>>[] changedProperties)
            => await UpdateAsync(customer, changedProperties: changedProperties);

        public async Task DeleteCustomerAsync(Guid id)
            => await DeleteAsync(c => c.Id == id);
    }
}
