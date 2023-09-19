using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandUpdateAdminPermissionMiddleware : AdminPermissionMiddleware<StoreCommandUpdateAdmin, StoreResult>
{
    public StoreCommandUpdateAdminPermissionMiddleware(IPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }
}
