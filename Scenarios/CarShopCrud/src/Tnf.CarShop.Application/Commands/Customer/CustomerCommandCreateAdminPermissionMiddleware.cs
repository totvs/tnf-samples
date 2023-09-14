using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands.Customer;
public class CustomerCommandCreateAdminPermissionMiddleware : AdminPermissionMiddleware<CustomerCommandCreateAdmin, CustomerResult>
{
    public CustomerCommandCreateAdminPermissionMiddleware(IPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }
}
