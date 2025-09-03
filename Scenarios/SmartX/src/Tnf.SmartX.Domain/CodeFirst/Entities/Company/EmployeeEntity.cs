namespace Tnf.SmartX.Domain.CodeFirst.Entities;

public class EmployeeEntity : SXObject
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string Position { get; set; }
    public Guid TeamId { get; set; }
    public TeamEntity Team { get; set; }
}
