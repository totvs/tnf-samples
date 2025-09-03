using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.SmartX.Domain.CodeFirst.Entities;

public class CompanyEntity : SXObject, IMustHaveTenant, IHasModificationTime, IHasCreationTime
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string TradeName { get; set; }
    public string RegistrationNumber { get; set; }
    public bool HasEsg { get; set; }
    public string Email { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public List<DepartmentEntity> Departments { get; set; } = [];
}
