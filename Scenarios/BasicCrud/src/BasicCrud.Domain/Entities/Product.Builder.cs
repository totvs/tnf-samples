using BasicCrud.Domain.Entities.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;

namespace BasicCrud.Domain.Entities
{
    public partial class Product
    {
        public class Builder : Builder<Product>
        {
            public Builder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public Builder(INotificationHandler notificationHandler, Product instance)
                : base(notificationHandler, instance)
            {
            }

            public Builder WithId(Guid id)
            {
                Instance.Id = id;
                return this;
            }

            public Builder WithDescription(string description)
            {
                Instance.Description = description;
                return this;
            }

            public Builder WithValue(float value)
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
