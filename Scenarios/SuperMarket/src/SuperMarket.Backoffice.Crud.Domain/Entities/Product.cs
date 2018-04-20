using SuperMarket.Backoffice.Crud.Domain.Entities.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Crud.Domain.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }

        public static ProductBuilder New(INotificationHandler notificationHandler)
            => new ProductBuilder(notificationHandler);

        public enum Error
        {
            ProductMustHaveDescription,
            ProductMustHaveValue
        }

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

            public ProductBuilder WithValue(decimal value)
            {
                Instance.Value = value;
                return this;
            }

            protected override void Specifications()
            {
                base.Specifications();

                AddSpecification<ProductMustHaveDescription>();
                AddSpecification<ProductMustHaveValue>();
            }
        }
    }
}
