using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Notifications;

namespace BasicCrud.Domain.Tests.Mocks
{
    public class ProductRepositoryMock : IProductRepository
    {
        private readonly INotificationHandler notificationHandler;

        public static Guid productGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<Product> list = new List<Product>();

        public ProductRepositoryMock(INotificationHandler notificationHandler)
        {
            this.notificationHandler = notificationHandler;

            list.Add(Product.Create(notificationHandler).WithId(productGuid).WithDescription("Product A").WithValue(5).Build());
            list.Add(Product.Create(notificationHandler).WithId(Guid.NewGuid()).WithDescription("Product B").WithValue(10).Build());
            list.Add(Product.Create(notificationHandler).WithId(Guid.NewGuid()).WithDescription("Product C").WithValue(15).Build());
        }

        public Task DeleteProductAsync(Guid id)
        {
            list.RemoveAll(c => c.Id == id);

            return Task.CompletedTask;
        }

        public Task<Product> InsertProductAndGetIdAsync(Product product)
        {
            product.Id = Guid.NewGuid();

            list.Add(product);

            return product.AsTask();
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            list.RemoveAll(c => c.Id == product.Id);
            list.Add(product);

            return product.AsTask();
        }
    }
}
