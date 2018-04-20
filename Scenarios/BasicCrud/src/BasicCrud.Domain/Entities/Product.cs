using System;
using Tnf.Notifications;

namespace BasicCrud.Domain.Entities
{
    public partial class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Description { get; internal set; }

        public float Value { get; internal set; }

        public static Builder Create(INotificationHandler handler)
            => new Builder(handler);

        public static Builder Create(INotificationHandler handler, Product instance)
            => new Builder(handler, instance);
    }
}
