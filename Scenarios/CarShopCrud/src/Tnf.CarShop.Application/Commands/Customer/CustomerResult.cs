using Tnf.CarShop.Domain.Dtos;

namespace Tnf.CarShop.Application.Commands.Customer;
public class CustomerResult : AdminResult
{
    public CustomerDto CustomerDto { get; set; }
}
