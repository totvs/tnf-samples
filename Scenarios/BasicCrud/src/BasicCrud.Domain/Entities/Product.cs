using BasicCrud.Domain.Entities.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace BasicCrud.Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public string Description { get; internal set; }

        public float Value { get; internal set; }

        public enum Error
        {
            ProductShouldHaveDescription,
            ProductShouldHaveValue
        }

        public static ProductBuilder Create(INotificationHandler handler)
            => new ProductBuilder(handler);

        public static ProductBuilder Create(INotificationHandler handler, Product instance)
            => new ProductBuilder(handler, instance);

        public class ProductBuilder : Builder<Product>
        {
            public ProductBuilder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public ProductBuilder(INotificationHandler notificationHandler, Product instance)
                : base(notificationHandler, instance)
            {
            }

            public ProductBuilder WithId(Guid id)
            {
                Instance.Id = id;
                return this;
            }

            public ProductBuilder WithDescription(string description)
            {
                Instance.Description = description;
                return this;
            }

            public ProductBuilder WithValue(float value)
            {
                Instance.Value = value;
                return this;
            }

            protected override void Specifications()
            {
                AddSpecification<ProductShouldHaveDescriptionSpecification>();
                AddSpecification<ProductShouldHaveValueSpecification>();
            }
        }
    }
}
