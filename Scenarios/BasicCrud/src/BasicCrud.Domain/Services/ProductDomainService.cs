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
        private readonly IProductRepository _repository;

        public ProductDomainService(IProductRepository repository, INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            _repository = repository;
        }

        public Task DeleteProductAsync(Guid id) => _repository.DeleteProductAsync(id);

        public async Task<Product> InsertProductAsync(Product.Builder builder)
        {
            if (builder == null)
            {
                Notification
                    .DefaultBuilder
                    .AsError()
                    .FromErrorEnum(Error.DomainServiceOnInsertNullBuilderError)
                    .WithMessage(Constants.LocalizationSourceName, Error.DomainServiceOnInsertNullBuilderError)
                    .Raise();

                return default(Product);
            }

            var product = builder.Build();

            if (Notification.HasNotification())
                return default(Product);

            product = await _repository.InsertProductAndGetIdAsync(product);

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product.Builder builder)
        {
            if (builder == null)
            {
                Notification
                    .DefaultBuilder
                    .AsError()
                    .FromErrorEnum(Error.DomainServiceOnUpdateNullBuilderError)
                    .WithMessage(Constants.LocalizationSourceName, Error.DomainServiceOnUpdateNullBuilderError)
                    .Raise();

                return default(Product);
            }

            var product = builder.Build();

            if (Notification.HasNotification())
                return default(Product);

            return await _repository.UpdateProductAsync(product);
        }
    }
}
