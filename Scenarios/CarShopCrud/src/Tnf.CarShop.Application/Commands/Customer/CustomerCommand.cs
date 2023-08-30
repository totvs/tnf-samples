using Tnf.CarShop.Domain.Dtos;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer;

//Implementar um ICommand<TResult> é opcional, porém uma boa prática por convenção!
public class CustomerCommand: ICommand<CustomerResult>
{
    public Guid? Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid StoreId { get; set; }
}

public class CustomerResult
{
    public CustomerDto CustomerDto { get; set; }
}
