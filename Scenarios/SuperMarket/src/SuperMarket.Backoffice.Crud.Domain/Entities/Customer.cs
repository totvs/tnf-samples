using System;
using Tnf.Builder;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace SuperMarket.Backoffice.Crud.Domain.Entities
{
    public class Customer : Entity<Guid>
    {
        public string Name { get; set; }

        public static CustomerBuilder New(INotificationHandler notificationHandler)
            => new CustomerBuilder(notificationHandler);

        public static CustomerBuilder Update(INotificationHandler notificationHandler, Customer instance)
            => new CustomerBuilder(notificationHandler, instance);

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

            protected override void Specifications()
            {
                base.Specifications();

                
            }
        }
    }
}
