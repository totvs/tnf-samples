using System;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace BasicCrud.Domain.Entities
{
    public partial class Product : Entity<Guid>
    {
        public string Description { get; internal set; }

        public float Value { get; internal set; }

        public static Builder Create(INotificationHandler handler)
            => new Builder(handler);

        public static Builder Create(INotificationHandler handler, Product instance)
            => new Builder(handler, instance);
    }
}
