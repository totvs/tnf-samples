using System;

namespace RedisCache.CachedValues
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }

        public Product()
        {
        }

        public Product(Guid id, string description, Category category)
        {
            Id = id;
            Description = description;
            Category = category;
        }
    }
}
