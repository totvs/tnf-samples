using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace TnfZero.Domain.Entities;

public class DogEntity : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{
    protected DogEntity()
    {
    }

    public DogEntity(string name)
    {
        Name = name;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public Guid TenantId { get; set; }
}