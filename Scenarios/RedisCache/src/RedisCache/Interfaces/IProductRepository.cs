using System;
using System.Collections.Generic;
using RedisCache.CachedValues;

namespace RedisCache.Interfaces
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(Guid id);
        Product Get(Guid id);
        IEnumerable<Product> GetProducts();
    }
}