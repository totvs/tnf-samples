using Microsoft.Extensions.Logging;
using RedisCache.CachedValues;
using RedisCache.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.Caching;

namespace RedisCache
{
    public class ProductService : IProductService
    {
        private readonly ICache cache;
        private readonly ILogger<ProductService> logger;
        private readonly IProductRepository productRepository;

        public ProductService(ICache cache, ILogger<ProductService> logger, IProductRepository productRepository)
        {
            this.cache = cache;
            this.logger = logger;
            this.productRepository = productRepository;
        }

        public void Add(Product product)
        {
            productRepository.Add(product);

            logger.LogInformation("Product added");

            cache.DeleteKey(Constants.AllProductsCacheKey);
        }

        public void Update(Product product)
        {
            productRepository.Update(product);

            logger.LogInformation("Product updated");

            cache.DeleteKey(Constants.AllProductsCacheKey);
        }

        public void Delete(Guid id)
        {
            productRepository.Delete(id);

            logger.LogInformation("Product deleted");

            cache.DeleteKey(Constants.AllProductsCacheKey);
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = cache.GetOrAdd(Constants.AllProductsCacheKey, () =>
            {
                logger.LogInformation("Products cache reload");

                var reload = productRepository.GetProducts();

                logger.LogInformation(string.Join(", ", reload.Select(s => s.Description)));

                return reload;

            }, Constants.AbsoluteExpiration);

            return products;
        }

        public Product GetProduct(Guid id)
        {
            return productRepository.Get(id);
        }
    }
}
