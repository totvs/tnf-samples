using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tnf.Builder;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Tests.Mocks
{
    public class CustomerDomainServiceMock : IDomainService<Customer, Guid>
    {
        public static Guid customerGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<Customer> list = new List<Customer>() {
            new Customer() { Id = customerGuid, Name = "Customer A" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer B" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer C" }
        };

        public string EntityName { get; set; }


        public void Delete(Guid id)
        {
            list.RemoveAll(c => c.Id == id);
        }

        public void Delete(Customer entity)
            => Delete(entity.Id);

        public Task DeleteAsync(Guid id)
        {
            Delete(id);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Customer entity)
            => DeleteAsync(entity.Id);


        public Guid InsertAndGetId<TBuilder>(TBuilder builder) where TBuilder : IBuilder<Customer>
        {
            var entity = builder.Build();

            entity.Id = Guid.NewGuid();
            list.Add(entity);

            return entity.Id;
        }

        public Task<Guid> InsertAndGetIdAsync<TBuilder>(TBuilder builder) where TBuilder : IBuilder<Customer>
            => InsertAndGetId(builder).AsTask();


        public Customer Update<TBuilder>(TBuilder builder) where TBuilder : IBuilder<Customer>
        {
            var entity = builder.Build();

            list.RemoveAll(c => c.Id == entity.Id);
            list.Add(entity);

            return entity;
        }

        public Task<Customer> UpdateAsync<TBuilder>(TBuilder builder) where TBuilder : IBuilder<Customer>
            => Update(builder).AsTask();


        public Customer Get(IRequestDto<Guid> key, bool include = true, bool select = true)
        {
            var entity = list.FirstOrDefault(c => c.Id == key.GetId());

            return entity;
        }

        public TDto Get<TDto>(IRequestDto<Guid> key, bool include = true, bool select = true) where TDto : IDto<Guid>
            => Get(key).MapTo<TDto>();

        public Task<Customer> GetAsync(IRequestDto<Guid> key, bool include = true, bool select = true)
            => Get(key).AsTask();

        public Task<TDto> GetAsync<TDto>(IRequestDto<Guid> key, bool include = true, bool select = true) where TDto : IDto<Guid>
            => Get<TDto>(key).AsTask();


        public IListDto<TDto, Guid> GetAll<TDto>(IRequestAllDto key, Expression<Func<Customer, bool>> func = null, bool paging = true, bool orderning = true) where TDto : IDto<Guid>
        {
            IListDto<TDto, Guid> result = new ListDto<TDto, Guid> { HasNext = false, Items = list.MapTo<List<TDto>>() };

            return result;
        }

        public Task<IListDto<TDto, Guid>> GetAllAsync<TDto>(IRequestAllDto key, Expression<Func<Customer, bool>> func = null, bool paging = true, bool orderning = true) where TDto : IDto<Guid>
            => GetAll<TDto>(key).AsTask();
    }
}
