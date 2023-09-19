using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands.Customer;
public class CustomerCommandUpdateAdminPermissionMiddleware : AdminPermissionMiddleware<CustomerCommandUpdateAdmin, CustomerResult>
{
    public CustomerCommandUpdateAdminPermissionMiddleware(IPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }
}
