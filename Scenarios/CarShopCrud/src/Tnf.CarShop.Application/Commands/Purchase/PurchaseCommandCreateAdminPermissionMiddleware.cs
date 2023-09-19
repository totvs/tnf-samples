using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands.Purchase;
public class CustomerCommandCreateAdminPermissionMiddleware : AdminPermissionMiddleware<PurchaseCommandCreateAdmin, PurchaseResult>
{
    public CustomerCommandCreateAdminPermissionMiddleware(IPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }
}
