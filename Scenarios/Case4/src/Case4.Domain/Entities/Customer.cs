using Case4.Domain.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace Case4.Domain
{
    public class Customer : Entity<Guid>
    {
        public string Name { get; internal set; }
        public string Email { get; internal set; }

        public enum Error
        {
            CustomerShouldHaveName,
            CustomerShouldHaveEmail
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

            public CustomerBuilder WithEmail(string email)
            {
                Instance.Email = email;
                return this;
            }

            protected override void Specifications()
            {
                AddSpecificationWithParamsForLocalizationKey(new CustomerShouldHaveNameSpecification(), "Customer");
            }
        }
    }
}
