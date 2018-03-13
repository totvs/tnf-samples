using BasicCrud.Domain.Entities.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace BasicCrud.Domain.Entities
{
    public class Customer : Entity<Guid>
    {
        public string Name { get; internal set; }

        public enum Error
        {
            CustomerShouldHaveName
        }

        public static CustomerBuilder Create(INotificationHandler handler)
            => new CustomerBuilder(handler);

        public static CustomerBuilder Create(INotificationHandler handler, Customer instance)
            => new CustomerBuilder(handler, instance);

        public class CustomerBuilder : Builder<Customer>
        {
            public CustomerBuilder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public CustomerBuilder(INotificationHandler notificationHandler, Customer instance)
                : base(notificationHandler, instance)
            {
            }

            public CustomerBuilder WithId(Guid id)
            {
                Instance.Id = id;
                return this;
            }

            public CustomerBuilder WithName(string name)
            {
                Instance.Name = name;
                return this;
            }

            protected override void Specifications()
            {
                AddSpecification<CustomerShouldHaveNameSpecification>();
            }
        }
    }
}
