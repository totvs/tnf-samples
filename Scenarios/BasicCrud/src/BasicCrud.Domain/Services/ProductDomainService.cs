using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Repositories;
using BasicCrud.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Notifications;

namespace BasicCrud.Domain.Services
{
    public class ProductDomainService : DomainService, IProductDomainService
    {
        private readonly IProductRepository repository;

        public ProductDomainService(IProductRepository repository, INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            this.repository = repository;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await repository.DeleteProductAsync(id);
        }

        public async Task<Product> InsertProductAsync(Product.ProductBuilder builder)
        {
            if(builder == null)
            {
                Notification
                    .DefaultBuilder
                    .AsError()
                    .FromErrorEnum(Error.DomainServiceOnInsertAndGetIdNullBuilderError)
                    .WithMessage(DomainConstants.LocalizationSourceName, Error.DomainServiceOnInsertAndGetIdNullBuilderError)
                    .Raise();

                return default(Product);
            }

            var product = builder.Build();

            if (Notification.HasNotification())
                return default(Product);

            product.Id = await repository.InsertProductAndGetIdAsync(product);

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product.ProductBuilder builder)
        {
            if (builder == null)
            {
                Notification
                    .DefaultBuilder
                    .AsError()
                    .FromErrorEnum(Error.DomainServiceOnUpdateNullBuilderError)
                    .WithMessage(DomainConstants.LocalizationSourceName, Error.DomainServiceOnUpdateNullBuilderError)
                    .Raise();

                return default(Product);
            }

            var product = builder.Build();

            if (Notification.HasNotification())
                return default(Product);

            return await repository.UpdateProductAsync(product);
        }
    }
}
