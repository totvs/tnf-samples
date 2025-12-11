using System;
using System.Collections.Generic;
using RedisCache.CachedValues;

namespace RedisCache.Interfaces
{
    public interface IProductService
    {
        void Add(Product product);
        void Delete(Guid id);
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts();
        void Update(Product product);
    }
}