namespace Tnf.SmartX.Domain.CodeFirst.Entities;

public class TeamEntity : SXObject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DepartmentId { get; set; }
    public DepartmentEntity Department { get; set; }
    public List<EmployeeEntity> Employees { get; set; } = [];
}
