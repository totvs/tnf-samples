using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Services;
using BasicCrud.Dto;
using BasicCrud.Dto.Product;
using BasicCrud.Infra.ReadInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Notifications;

namespace BasicCrud.Application.Tests.Mocks
{
    public class ProductServiceMock : IProductDomainService, IProductReadRepository
    {
        private readonly INotificationHandler notificationHandler;

        public static Guid productGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<Product> list = new List<Product>();

        public string EntityName { get; set; }

        public ProductServiceMock(INotificationHandler notificationHandler)
        {
            this.notificationHandler = notificationHandler;

            list.Add(Product.Create(notificationHandler).WithId(productGuid).WithDescription("Product A").WithValue(5).Build());
            list.Add(Product.Create(notificationHandler).WithId(Guid.NewGuid()).WithDescription("Product B").WithValue(10).Build());
            list.Add(Product.Create(notificationHandler).WithId(Guid.NewGuid()).WithDescription("Product C").WithValue(15).Build());
        }

        public Task<Product> InsertProductAsync(Product.Builder builder)
        {
            var entity = builder.Build();

            if (notificationHandler.HasNotification())
                return Task.FromResult<Product>(null);

            entity.Id = Guid.NewGuid();

            list.Add(entity);

            return entity.AsTask();
        }

        public Task<Product> UpdateProductAsync(Product.Builder builder)
        {
            var entity = builder.Build();

            if (notificationHandler.HasNotification())
                return Task.FromResult<Product>(null);

            list.RemoveAll(c => c.Id == entity.Id);
            list.Add(entity);

            return entity.AsTask();
        }

        public Task DeleteProductAsync(Guid id)
        {
            list.RemoveAll(c => c.Id == id);

            return Task.CompletedTask;
        }

        public Task<ProductDto> GetProductAsync(DefaultRequestDto key)
            => list.FirstOrDefault(c => c.Id == key.Id).MapTo<ProductDto>().AsTask();

        public Task<IListDto<ProductDto>> GetAllProductsAsync(ProductRequestAllDto key)
            => list.ToListDto<Product, ProductDto>(false).AsTask();
    }
}
