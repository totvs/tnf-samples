using Tnf.Repositories.Entities.Auditing;

namespace Tnf.SmartX.Domain.CodeFirst.Entities;

public class AddressEntity : SXObject, IHasModificationTime, IHasCreationTime
{
    public Guid Id { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Street { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public List<DeliveryEntity> Deliveries { get; set; } = [];

    public Guid CustomerId { get; set; }
    public CustomerEntity Customer { get; set; }
}
