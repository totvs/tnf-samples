using System;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace Security.Domain.Entities
{
    public partial class Customer : IEntity, IMustHaveTenant
    {
        public Guid Id { get; set; }
        public string Name { get; internal set; }
        public int TenantId { get; set; }

        public static Builder Create(INotificationHandler handler)
            => new Builder(handler);

        public static Builder Create(INotificationHandler handler, Customer instance)
            => new Builder(handler, instance);
    }
}
