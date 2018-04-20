using BasicCrud.Domain.Entities;
using BasicCrud.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tnf.Builder;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace BasicCrud.Application.Tests.Mocks
{
    public class CustomerDomainServiceMock : IDomainService<Customer>
    {
        private readonly INotificationHandler _notificationHandler;

        public static Guid customerGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<Customer> list = new List<Customer>();

        public string EntityName { get; set; }

        public CustomerDomainServiceMock(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;

            list.Add(Customer.Create(notificationHandler).WithId(customerGuid).WithName("Customer A").Build());
            list.Add(Customer.Create(notificationHandler).WithId(Guid.NewGuid()).WithName("Customer B").Build());
            list.Add(Customer.Create(notificationHandler).WithId(Guid.NewGuid()).WithName("Customer C").Build());
        }

        public void Delete(Customer entity)
            => Delete(w => w.Id == entity.Id);

        public Task DeleteAsync(Customer entity)
        {
            Delete(entity);

            return Task.CompletedTask;
        }

        public Task<Customer> GetAsync(IRequestDto key, bool include = true, bool select = true)
            => Get(key, include, select).AsTask();

        public Task<TDto> GetAsync<TDto>(IRequestDto key, bool include = true, bool select = true)
            where TDto : IDto
        {
            var customer = Get(key, include, select);
            return customer.MapTo<TDto>().AsTask();
        }

        public Customer InsertAndSaveChanges<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Customer>
        {
            var entity = builder.Build();

            if (_notificationHandler.HasNotification())
                return null;

            entity.Id = Guid.NewGuid();

            list.Add(entity);

            return entity;
        }

        public Task<Customer> InsertAndSaveChangesAsync<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Customer>
            => InsertAndSaveChanges(builder).AsTask();

        public Customer Update<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Customer>
        {
            var entity = builder.Build();

            if (_notificationHandler.HasNotification())
                return null;

            list.RemoveAll(c => c.Id == entity.Id);
            list.Add(entity);

            return entity;
        }

        public Task<Customer> UpdateAsync<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Customer>
            => Update(builder).AsTask();

        public void Delete(Expression<Func<Customer, bool>> id)
        {
            list.RemoveAll(id.Compile());
        }

        public Task DeleteAsync(Expression<Func<Customer, bool>> id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public Customer Get<TRequestDto>(TRequestDto key, bool include = true, bool select = true) 
            where TRequestDto : IRequestDto
        {
            var defaultRequestDto = key as DefaultRequestDto;

            var customer = list.FirstOrDefault(w => w.Id == defaultRequestDto.Id);

            return customer;
        }

        public TDto Get<TDto, TRequestDto>(TRequestDto key, bool include = true, bool select = true) where TRequestDto : IRequestDto
        {
            var defaultRequestDto = key as DefaultRequestDto;

            var customer = list.FirstOrDefault(w => w.Id == defaultRequestDto.Id);

            return customer.MapTo<TDto>();
        }

        public Task<Customer> GetAsync<TRequestDto>(TRequestDto key, bool include = true, bool select = true) where TRequestDto : IRequestDto
            => Get(key, include, select).AsTask();

        public Task<TDto> GetAsync<TDto, TRequestDto>(TRequestDto key, bool include = true, bool select = true) where TRequestDto : IRequestDto
            => Get<TDto, TRequestDto>(key, include, select).AsTask();

        public IListDto<TDto> GetAll<TDto>(IRequestAllDto key, Expression<Func<Customer, bool>> func = null, bool paging = true, bool orderning = true)
        {
            var result = list.ToListDto<Customer, TDto>(false);
            return result;
        }

        public Task<IListDto<TDto>> GetAllAsync<TDto>(IRequestAllDto key, Expression<Func<Customer, bool>> func = null, bool paging = true, bool orderning = true)
            => GetAll<TDto>(key, func, paging, orderning).AsTask();
    }
}
