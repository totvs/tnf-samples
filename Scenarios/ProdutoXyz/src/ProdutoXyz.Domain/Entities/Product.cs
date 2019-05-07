using System;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace ProdutoXyz.Domain.Entities
{
    public partial class Product : IEntity, IMustHaveTenant
    {
        public Guid Id { get; set; }
        public string Description { get; internal set; }

        public float Value { get; internal set; }
        public Guid TenantId { get; set; }

        public static Builder Create(INotificationHandler handler)
            => new Builder(handler);

        public static Builder Create(INotificationHandler handler, Product instance)
            => new Builder(handler, instance);
    }
}
