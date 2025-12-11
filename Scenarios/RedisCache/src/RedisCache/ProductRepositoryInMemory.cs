using RedisCache.CachedValues;
using RedisCache.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisCache
{
    public class ProductRepositoryInMemory : IProductRepository
    {
        public static Guid ProductKey = Guid.NewGuid();
        public static Category DefaultCategory = new Category(1, "Todos Produtos");

        public List<Product> InMemoryValues = new List<Product>()
        {
            new Product(ProductKey, "Boné", DefaultCategory),
            new Product(Guid.NewGuid(), "Sapato", DefaultCategory),
            new Product(Guid.NewGuid(), "Camiseta", DefaultCategory),
            new Product(Guid.NewGuid(), "Calça", DefaultCategory),
            new Product(Guid.NewGuid(), "Chinelo", DefaultCategory),
        };

        public void Add(Product product)
        {
            InMemoryValues.Add(product);
        }

        public void Delete(Guid id)
        {
            InMemoryValues.RemoveAll(w => w.Id == id);
        }

        public Product Get(Guid id)
            => InMemoryValues.FirstOrDefault(w => w.Id == id);

        public IEnumerable<Product> GetProducts()
            => InMemoryValues;

        public void Update(Product product)
        {
            InMemoryValues.RemoveAll(w => w.Id == product.Id);

            InMemoryValues.Add(product);
        }
    }
}
