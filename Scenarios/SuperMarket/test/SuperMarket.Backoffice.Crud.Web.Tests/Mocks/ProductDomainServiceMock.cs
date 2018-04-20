using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tnf.Builder;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Crud.Web.Tests.Mocks
{
    public class ProductDomainServiceMock : IDomainService<Product>
    {
        private readonly INotificationHandler _notificationHandler;

        public static Guid productGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<Product> list = new List<Product>();

        public string EntityName { get; set; }

        public ProductDomainServiceMock(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;

            list.Add(Product.New(notificationHandler).WithId(productGuid).WithDescription("Product A").WithValue(5).Build());
            list.Add(Product.New(notificationHandler).WithId(productGuid).WithDescription("Product B").WithValue(10).Build());
            list.Add(Product.New(notificationHandler).WithId(productGuid).WithDescription("Product C").WithValue(20).Build());
        }

        public void Delete(Product entity)
           => Delete(w => w.Id == entity.Id);

        public Task DeleteAsync(Product entity)
        {
            Delete(entity);

            return Task.CompletedTask;
        }

        public Task<Product> GetAsync(IRequestDto key, bool include = true, bool select = true)
            => Get(key, include, select).AsTask();

        public Task<TDto> GetAsync<TDto>(IRequestDto key, bool include = true, bool select = true)
            where TDto : IDto
        {
            var customer = Get(key, include, select);
            return customer.MapTo<TDto>().AsTask();
        }

        public Product InsertAndSaveChanges<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Product>
        {
            var entity = builder.Build();

            if (_notificationHandler.HasNotification())
                return null;

            entity.Id = Guid.NewGuid();

            list.Add(entity);

            return entity;
        }

        public Task<Product> InsertAndSaveChangesAsync<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Product>
            => InsertAndSaveChanges(builder).AsTask();

        public Product Update<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Product>
        {
            var entity = builder.Build();

            if (_notificationHandler.HasNotification())
                return null;

            list.RemoveAll(c => c.Id == entity.Id);
            list.Add(entity);

            return entity;
        }

        public Task<Product> UpdateAsync<TBuilder>(TBuilder builder)
            where TBuilder : IBuilder<Product>
            => Update(builder).AsTask();

        public void Delete(Expression<Func<Product, bool>> id)
        {
            list.RemoveAll(id.Compile());
        }

        public Task DeleteAsync(Expression<Func<Product, bool>> id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public Product Get<TRequestDto>(TRequestDto key, bool include = true, bool select = true)
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

        public Task<Product> GetAsync<TRequestDto>(TRequestDto key, bool include = true, bool select = true) where TRequestDto : IRequestDto
            => Get(key, include, select).AsTask();

        public Task<TDto> GetAsync<TDto, TRequestDto>(TRequestDto key, bool include = true, bool select = true) where TRequestDto : IRequestDto
            => Get<TDto, TRequestDto>(key, include, select).AsTask();

        public IListDto<TDto> GetAll<TDto>(IRequestAllDto key, Expression<Func<Product, bool>> func = null, bool paging = true, bool orderning = true)
        {
            var result = list.ToListDto<Product, TDto>(false);
            return result;
        }

        public Task<IListDto<TDto>> GetAllAsync<TDto>(IRequestAllDto key, Expression<Func<Product, bool>> func = null, bool paging = true, bool orderning = true)
            => GetAll<TDto>(key, func, paging, orderning).AsTask();
    }
}
