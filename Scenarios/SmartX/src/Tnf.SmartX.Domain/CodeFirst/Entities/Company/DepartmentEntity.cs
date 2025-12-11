using Tnf.Repositories.Entities.Auditing;

namespace Tnf.SmartX.Domain.CodeFirst.Entities;

public class DepartmentEntity : SXObject, IHasModificationTime, IHasCreationTime
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public DateTime CreationTime { get; set; }

    public CompanyEntity Company { get; set; }
    public List<TeamEntity> Teams { get; set; } = [];
}
