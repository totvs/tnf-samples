using BasicCrud.Domain.Entities;
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
    public class DomainServiceMock : IDomainService<Customer, Guid>
    {
        private readonly INotificationHandler notificationHandler;

        public static Guid customerGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<Customer> list = new List<Customer>();

        public string EntityName { get; set; }

        public DomainServiceMock(INotificationHandler notificationHandler)
        {
            this.notificationHandler = notificationHandler;

            list.Add(Customer.Create(notificationHandler).WithId(customerGuid).WithName("Customer A").Build());
            list.Add(Customer.Create(notificationHandler).WithId(Guid.NewGuid()).WithName("Customer B").Build());
            list.Add(Customer.Create(notificationHandler).WithId(Guid.NewGuid()).WithName("Customer C").Build());
        }

        public void Delete(Customer entity)
            => Delete(entity.Id);

        public void Delete(Guid id)
        {
            list.RemoveAll(c => c.Id == id);
        }

        public Task DeleteAsync(Guid id)
        {
            Delete(id);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Customer entity)
        {
            Delete(entity);

            return Task.CompletedTask;
        }

        public Customer Get(IRequestDto<Guid> key, bool include = true, bool select = true)
            => list.FirstOrDefault(c => c.Id == key.GetId());

        public TDto Get<TDto>(IRequestDto<Guid> key, bool include = true, bool select = true)
            where TDto : IDto<Guid>
            => list.FirstOrDefault(c => c.Id == key.GetId()).MapTo<TDto>();

        public IListDto<TDto, Guid> GetAll<TDto>(IRequestAllDto key, Expression<Func<Customer, bool>> func = null, bool paging = true, bool orderning = true)
            where TDto : IDto<Guid>
            => list.ToListDto<Customer, TDto, Guid>(false);

        public async Task<IListDto<TDto, Guid>> GetAllAsync<TDto>(IRequestAllDto key, Expression<Func<Customer, bool>> func = null, bool paging = true, bool orderning = true)
            where TDto : IDto<Guid>
            => await list.ToListDtoAsync<Customer, TDto, Guid>(false);

        public Task<Customer> GetAsync(IRequestDto<Guid> key, bool include = true, bool select = true)
            => Get(key).AsTask();

        public Task<TDto> GetAsync<TDto>(IRequestDto<Guid> key, bool include = true, bool select = true)
            where TDto : IDto<Guid>
            => Get<TDto>(key).AsTask();

        public Guid InsertAndGetId<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Customer>
        {
            var entity = builder.Build();

            if (notificationHandler.HasNotification())
                return Guid.Empty;

            entity.Id = Guid.NewGuid();

            list.Add(entity);

            return entity.Id;
        }

        public Task<Guid> InsertAndGetIdAsync<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Customer>
            => InsertAndGetId(builder).AsTask();

        public Customer Update<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Customer>
        {
            var entity = builder.Build();

            if (notificationHandler.HasNotification())
                return null;

            list.RemoveAll(c => c.Id == entity.Id);
            list.Add(entity);

            return entity;
        }

        public Task<Customer> UpdateAsync<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Customer>
            => Update(builder).AsTask();
    }
}
