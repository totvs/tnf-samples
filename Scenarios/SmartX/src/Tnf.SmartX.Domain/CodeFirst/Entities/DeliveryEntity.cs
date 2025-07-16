using Tnf.Repositories.Entities.Auditing;

namespace Tnf.SmartX.Domain.CodeFirst.Entities;

public class DeliveryEntity : SXObject, IHasModificationTime, IHasCreationTime
{
    public Guid Id { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string Status { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public List<DeliveryItemEntity> DeliveryItems { get; set; } = [];

    public Guid AddressId { get; set; }
    public AddressEntity Address { get; set; }
}
