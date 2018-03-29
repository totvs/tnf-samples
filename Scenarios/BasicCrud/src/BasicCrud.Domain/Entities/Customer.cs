using System;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace BasicCrud.Domain.Entities
{
    public partial class Customer : Entity<Guid>
    {
        public string Name { get; internal set; }

        public static Builder Create(INotificationHandler handler)
            => new Builder(handler);

        public static Builder Create(INotificationHandler handler, Customer instance)
            => new Builder(handler, instance);
    }
}
