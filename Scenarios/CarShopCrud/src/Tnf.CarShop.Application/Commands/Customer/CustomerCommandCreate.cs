using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer;

public class CustomerCommandCreate : ICommand<CustomerResult>
{
    public Guid? Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid StoreId { get; set; }
}


public class CustomerCommandCreateAdmin : CustomerCommandCreate, IPermissionRequiredCommand
{
    public bool MustBeAdmin { get; set; }
}
