using Tnf.Repositories.Entities.Auditing;

namespace Tnf.SmartX.Domain.CodeFirst.Entities;

public class DeliveryItemEntity : SXObject, IHasModificationTime, IHasCreationTime
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal WeightKg { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public Guid DeliveryId { get; set; }
    public DeliveryEntity Delivery { get; set; }
}
