using ProdutoXyz.Domain.Entities;
using ProdutoXyz.Domain.Interfaces.Repositories;
using ProdutoXyz.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Notifications;

namespace ProdutoXyz.Domain.Services
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
                Notification.RaiseError(Constants.LocalizationSourceName, Error.DomainServiceOnInsertNullBuilderError);
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
                Notification.RaiseError(Constants.LocalizationSourceName, Error.DomainServiceOnUpdateNullBuilderError);
                return default(Product);
            }

            var product = builder.Build();

            if (Notification.HasNotification())
                return default(Product);

            return await _repository.UpdateProductAsync(product);
        }
    }
}
