using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands.Purchase;
public class CustomerCommandUpdateAdminPermissionMiddleware : AdminPermissionMiddleware<PurchaseCommandCreateAdmin, PurchaseResult>
{
    public CustomerCommandUpdateAdminPermissionMiddleware(IPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }
}
